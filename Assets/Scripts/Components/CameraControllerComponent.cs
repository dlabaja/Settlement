using Constants;
using Models.Controllers;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class CameraControllerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        private (KeyControl keyControl, Action<float> onPress)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private CameraZoomController _cameraZoomController;
        private InputAction _zoomAction;

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
                    //_cameraZoomController.StopZoom();
                    onPress(deltaTime);
                }
            }
        }

        private void ProcessZoom()
        {
            if (_zoomAction.WasPerformedThisFrame())
            {
                _cameraZoomController.StartZoom(_zoomAction.ReadValue<Vector2>().y);
            }
            
            if (!_cameraZoomController.ZoomEnded)
            {
                _cameraZoomController.Zoom();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            _cameraZoomController.StopZoom();
        }

        private void OnCollisionStay(Collision other)
        {
            _cameraZoomController.StopZoom();
        }

        public void Awake()
        {
            var cameraMap = InputSystem.actions.FindActionMap(InputActionMapName.Camera);
            var transform = _camera.GetComponent<Camera>().transform;
            _cameraZoomController = new CameraZoomController(transform, _camera.GetComponent<Rigidbody>());
            _cameraMovementController = new CameraMovementController(transform);
            _keyControlsWithAction = new (KeyControl keyControl, Action<float> onPress)[]
            {
                (GetKeyControl(cameraMap, InputActionName.CameraForward), _cameraMovementController.MoveForward),
                (GetKeyControl(cameraMap, InputActionName.CameraBackward), _cameraMovementController.MoveBackward),
                (GetKeyControl(cameraMap, InputActionName.CameraLeft), _cameraMovementController.MoveLeft),
                (GetKeyControl(cameraMap, InputActionName.CameraRight), _cameraMovementController.MoveRight)
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
