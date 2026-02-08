using Attributes;
using Constants;
using Instances;
using Managers;
using Models.Controllers.Camera;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Views.Camera;

namespace Components.Camera
{
    public class CameraControllerComponent : MonoBehaviour
    {
        [Autowired] private SettingsManager _settingsManager;
        [Autowired] private MousePositionManager _mousePositionManager;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private CameraZoomController _cameraZoomController;
        private CameraRotationController _cameraRotationController;
        private CameraControllerView _cameraControllerView;
        private InputAction _zoomAction;
        private KeyControl _allowRotationKey;

        public void Awake()
        {
            _cameraMovementController = new CameraMovementController(transform, _settingsManager);
            _cameraZoomController = new CameraZoomController(_settingsManager);
            _cameraRotationController = new CameraRotationController(_settingsManager);
            _cameraControllerView = new CameraControllerView(gameObject, _cameraMovementController, _cameraZoomController, _cameraRotationController);
            _keyControlsWithAction = new (KeyControl keyControl, Func<Vector3> action)[]
            {
                (GetKeyControl(InputActionName.CameraForward), _cameraMovementController.Forward),
                (GetKeyControl(InputActionName.CameraBackward), _cameraMovementController.Backward),
                (GetKeyControl(InputActionName.CameraLeft), _cameraMovementController.Left),
                (GetKeyControl(InputActionName.CameraRight), _cameraMovementController.Right),
            };
            _zoomAction = InputActionMaps.Camera.FindAction(InputActionName.CameraZoom);
            _allowRotationKey = GetKeyControl(InputActionName.CameraAllowRotate);
        }

        public void Update()
        {
            var movementDirection = Vector3.zero;
            foreach (var (keyControl, action) in _keyControlsWithAction)
            {
                if (keyControl.IsPressed)
                {
                    movementDirection += action();
                }
            }
            var movementVector = _cameraControllerView.MovementDelta(movementDirection);
            var zoomedVector = _cameraControllerView.ZoomDelta(_zoomAction.WasPerformedThisFrame(), _zoomAction.ReadValue<Vector2>().y);
            var rotation = _cameraControllerView.RotationDelta(_allowRotationKey.IsPressed, _mousePositionManager.Delta);
            _cameraControllerView.Process(movementVector, zoomedVector, rotation);
        }
        
        private KeyControl GetKeyControl(string actionName)
        {
            return new KeyControl(InputActionMaps.Camera.FindAction(actionName));
        }
        
        private void OnCollisionEnter()
        {
            _cameraZoomController.StopZoom();
        }

        private void OnCollisionStay()
        {
            _cameraZoomController.StopZoom();
        }
    }
}
