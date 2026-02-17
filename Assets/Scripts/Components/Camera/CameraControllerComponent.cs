using Attributes;
using Constants;
using Instances;
using Managers;
using Models.Camera;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Views.Camera;

namespace Components.Camera
{
    public class CameraControllerComponent : MonoBehaviour
    {
        [Autowired] private SettingsManager _settingsManager;
        [Autowired] private MousePositionManager _mousePositionManager;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovement _cameraMovement;
        private CameraZoom _cameraZoom;
        private CameraRotation _cameraRotation;
        private CameraControllerView _cameraControllerView;
        private InputAction _zoomAction;
        private KeyControl _allowRotationKey;

        public void Awake()
        {
            _cameraMovement = new CameraMovement(_settingsManager);
            _cameraZoom = new CameraZoom(_settingsManager);
            _cameraRotation = new CameraRotation(_settingsManager);
            _cameraControllerView = new CameraControllerView(gameObject, _cameraMovement, _cameraZoom, _cameraRotation);
            _keyControlsWithAction = new (KeyControl keyControl, Func<Vector3> action)[]
            {
                (GetKeyControl(InputActionName.CameraForward), transform.Forward),
                (GetKeyControl(InputActionName.CameraBackward), transform.Backward),
                (GetKeyControl(InputActionName.CameraLeft), transform.Left),
                (GetKeyControl(InputActionName.CameraRight), transform.Right),
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
            _cameraZoom.StopZoom();
        }

        private void OnCollisionStay()
        {
            _cameraZoom.StopZoom();
        }
    }
}
