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
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private CameraZoomController _cameraZoomController;
        private InputAction _zoomAction;

        private static KeyControl GetKeyControl(InputActionMap actionMap, string actionName)
        {
            return new KeyControl(actionMap.FindAction(actionName));
        }

        private void ProcessMovement(float deltaTime)
        {
            var vector = Vector3.zero;
            foreach (var (keyControl, action) in _keyControlsWithAction)
            {
                if (keyControl.IsPressed)
                {
                    vector += action();
                }
            }
            _cameraMovementController.Move(vector, deltaTime);
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
            var rigidbody = _camera.GetComponent<Rigidbody>();
            _cameraZoomController = new CameraZoomController(transform, rigidbody);
            _cameraMovementController = new CameraMovementController(transform, rigidbody);
            _keyControlsWithAction = new (KeyControl keyControl, Func<Vector3> action)[]
            {
                (GetKeyControl(cameraMap, InputActionName.CameraForward), action: _cameraMovementController.Forward),
                (GetKeyControl(cameraMap, InputActionName.CameraBackward), action: _cameraMovementController.Backward),
                (GetKeyControl(cameraMap, InputActionName.CameraLeft), action: _cameraMovementController.Left),
                (GetKeyControl(cameraMap, InputActionName.CameraRight), action: _cameraMovementController.Right)
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
