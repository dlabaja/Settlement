using Constants;
using Models.Controllers;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Components
{
    public class CameraControllerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private InputActionMap _actionMap;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private CameraZoomController _cameraZoomController;
        private CameraRotationController _cameraRotationController;
        private InputAction _zoomAction;
        private InputAction _rotateAction;
        private KeyControl _allowRotationKey;

        private KeyControl GetKeyControl(string actionName)
        {
            return new KeyControl(_actionMap.FindAction(actionName));
        }

        private Vector3 MovementVectorDelta()
        {
            var vector = Vector3.zero;
            foreach (var (keyControl, action) in _keyControlsWithAction)
            {
                if (keyControl.IsPressed)
                {
                    vector += action();
                }
            }
            return _cameraMovementController.MovedVectorDelta(vector, Time.deltaTime) - _camera.transform.position;
        }

        private Vector3 ZoomVectorDelta()
        {
            if (_zoomAction.WasPerformedThisFrame())
            {
                _cameraZoomController.StartZoom(_zoomAction.ReadValue<Vector2>().y);
            }
            
            return _cameraZoomController.ZoomedVectorDelta(Time.deltaTime);
        }

        private Vector3 RotationDelta()
        {
            return _allowRotationKey.IsPressed 
                ? _cameraRotationController.VectorToRotationDelta(_rotateAction.ReadValue<Vector2>(), Time.deltaTime) 
                : Vector3.zero;
        }

        private void OnCollisionEnter()
        {
            _cameraZoomController.StopZoom();
        }

        private void OnCollisionStay()
        {
            _cameraZoomController.StopZoom();
        }

        public void Awake()
        {
            _transform = _camera.GetComponent<Camera>().transform;
            _actionMap = InputSystem.actions.FindActionMap(InputActionMapName.Camera);
            _rigidbody = _camera.GetComponent<Rigidbody>();
            _cameraZoomController = new CameraZoomController(_transform);
            _cameraMovementController = new CameraMovementController(_transform);
            _cameraRotationController = new CameraRotationController(_transform);
            _keyControlsWithAction = new (KeyControl keyControl, Func<Vector3> action)[]
            {
                (GetKeyControl(InputActionName.CameraForward), _cameraMovementController.Forward),
                (GetKeyControl(InputActionName.CameraBackward), _cameraMovementController.Backward),
                (GetKeyControl(InputActionName.CameraLeft), _cameraMovementController.Left),
                (GetKeyControl(InputActionName.CameraRight), _cameraMovementController.Right),
            };
            _zoomAction = _actionMap.FindAction(InputActionName.CameraZoom);
            _rotateAction = _actionMap.FindAction(InputActionName.CameraRotate);
            _allowRotationKey = GetKeyControl(InputActionName.CameraAllowRotate);
        }

        public void Update()
        {
            var movementVector = MovementVectorDelta();
            var zoomedVector = ZoomVectorDelta();
            var rotation = RotationDelta();
            _rigidbody.Move(
                _camera.transform.position + movementVector + zoomedVector,
                Quaternion.Euler(_camera.transform.eulerAngles + rotation));
        }
    }
}
