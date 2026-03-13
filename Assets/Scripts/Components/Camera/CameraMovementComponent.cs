using Constants;
using Controllers.Camera;
using Instances;
using Models.Camera.Control;
using Models.Controls;
using Reflex.Attributes;
using Services;
using Services.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Views.Camera;

namespace Components.Camera
{
    public class CameraMovementComponent : MonoBehaviour
    {
        [Inject] private SettingsService _settingsService;
        [Inject] private MousePositionService _mousePositionService;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraControl _cameraControl;
        private CameraControlController _cameraControlController;
        private CameraControlView _cameraControlView;
        private InputAction _zoomAction;
        private KeyControl _allowRotationKey;

        public void Awake()
        {
            _cameraControl = new CameraControl(_settingsService);
            _keyControlsWithAction = new (KeyControl keyControl, Func<Vector3> action)[]
            {
                (GetKeyControl(InputActionName.CameraForward), transform.Forward),
                (GetKeyControl(InputActionName.CameraBackward), transform.Backward),
                (GetKeyControl(InputActionName.CameraLeft), transform.Left),
                (GetKeyControl(InputActionName.CameraRight), transform.Right),
            };
        }

        public void Start()
        {
            _cameraControlController = new CameraControlController(_cameraControl);
            _cameraControlView = new CameraControlView(_cameraControl, GetComponent<Rigidbody>(), transform);
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
            _cameraControlController.UpdateMovement(movementDirection, _zoomAction.ReadValue<Vector2>().y,
                _mousePositionService.Delta, transform.forward, _zoomAction.WasPerformedThisFrame(), 
                _allowRotationKey.IsPressed, Time.deltaTime);
        }
        
        private KeyControl GetKeyControl(string actionName)
        {
            return new KeyControl(InputActionMaps.Camera.FindAction(actionName));
        }
        
        private void OnCollisionEnter()
        {
            _cameraControlController.StopZoom();
        }

        private void OnCollisionStay()
        {
            _cameraControlController.StopZoom();
        }

        private void OnDestroy()
        {
            _cameraControlController.Dispose();
            _cameraControlView.Dispose();
        }
    }
}
