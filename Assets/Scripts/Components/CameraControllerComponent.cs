using Constants;
using Models.Controllers;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VectorUtils = Utils.VectorUtils;

namespace Components
{
    public class CameraControllerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        private (KeyControl keyControl, Action<float> onPress)[] _keyControlsWithAction;
        private CameraController _cameraController;
        private InputAction _zoomAction;
        private bool _zoomActive;
        private float _zoomFactor;
        private Vector3 _zoomVelocity;
        private Vector3 _zoomStartPost;

        private static KeyControl GetKeyControl(InputActionMap actionMap, string actionName)
        {
            return new KeyControl(actionMap.FindAction(actionName));
        }

        private void ProcessMovement(float deltaTime)
        {
            foreach (var (keyControl, onPress) in _keyControlsWithAction)
            {
                if (keyControl.IsPressed)
                {
                    onPress(deltaTime);
                }
            }
        }

        private void ProcessZoom()
        {
            if (_zoomAction.WasPerformedThisFrame() && !_zoomActive)
            {
                _zoomFactor = _zoomAction.ReadValue<Vector2>().y;
                _zoomActive = true;
                _zoomVelocity = _camera.transform.forward;
                _zoomStartPost = _camera.transform.position;
            }
            
            if (_zoomActive)
            {
                _cameraController.Zoom(_zoomFactor, _zoomStartPost, ref _zoomVelocity);
                _zoomActive = !VectorUtils.ApproxEql(_zoomVelocity, Vector3.zero, 0.1f);
            }
        }

        public void Awake()
        {
            var cameraMap = InputSystem.actions.FindActionMap(InputActionMapName.Camera);
            _cameraController = new CameraController(_camera.GetComponent<Camera>(), _camera.GetComponent<Rigidbody>());
            _keyControlsWithAction = new (KeyControl keyControl, Action<float> onPress)[]
            {
                (GetKeyControl(cameraMap, InputActionName.CameraForward), _cameraController.MoveForward),
                (GetKeyControl(cameraMap, InputActionName.CameraBackward), _cameraController.MoveBackward),
                (GetKeyControl(cameraMap, InputActionName.CameraLeft), _cameraController.MoveLeft),
                (GetKeyControl(cameraMap, InputActionName.CameraRight), _cameraController.MoveRight)
            };
            _zoomAction = cameraMap.FindAction(InputActionName.CameraZoom);
        }

        public void Update()
        {
            ProcessMovement(Time.deltaTime);
            ProcessZoom();
        }
    }
}
