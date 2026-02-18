using Constants;
using Controllers.Camera;
using Instances;
using Models.Controls;
using Reflex.Attributes;
using Services;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Components.Camera
{
    public class CameraMovementComponent : MonoBehaviour
    {
        [Inject] private SettingsService _settingsService;
        [Inject] private MousePositionService _mousePositionService;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private InputAction _zoomAction;
        private KeyControl _allowRotationKey;

        public void Awake()
        {
            _cameraMovementController = new CameraMovementController(GetComponent<Rigidbody>(), transform, _settingsService);
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
                _zoomAction.ReadValue<Vector2>().y, _allowRotationKey.IsPressed, _mousePositionService.Delta);
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
