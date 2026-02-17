using Attributes;
using Constants;
using Controllers.Camera;
using Instances;
using Managers;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Components.Camera
{
    public class CameraMovementComponent : MonoBehaviour
    {
        [Autowired] private SettingsManager _settingsManager;
        [Autowired] private MousePositionManager _mousePositionManager;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private InputAction _zoomAction;
        private KeyControl _allowRotationKey;

        public void Awake()
        {
            _cameraMovementController = new CameraMovementController(GetComponent<Rigidbody>(), transform, _settingsManager);
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
            _cameraMovementController.UpdateMovement(movementDirection, _zoomAction.WasPerformedThisFrame(), 
                _zoomAction.ReadValue<Vector2>().y, _allowRotationKey.IsPressed, _mousePositionManager.Delta);
        }
        
        private KeyControl GetKeyControl(string actionName)
        {
            return new KeyControl(InputActionMaps.Camera.FindAction(actionName));
        }
        
        private void OnCollisionEnter()
        {
            _cameraMovementController.StopZoom();
        }

        private void OnCollisionStay()
        {
            _cameraMovementController.StopZoom();
        }
    }
}
