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
        private Rigidbody _rigidbody;
        private (KeyControl keyControl, Func<Vector3> action)[] _keyControlsWithAction;
        private CameraMovementController _cameraMovementController;
        private CameraZoomController _cameraZoomController;
        private InputAction _zoomAction;

        private static KeyControl GetKeyControl(InputActionMap actionMap, string actionName)
        {
            return new KeyControl(actionMap.FindAction(actionName));
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
            var cameraMap = InputSystem.actions.FindActionMap(InputActionMapName.Camera);
            var transform = _camera.GetComponent<Camera>().transform;
            _rigidbody = _camera.GetComponent<Rigidbody>();
            _cameraZoomController = new CameraZoomController(transform);
            _cameraMovementController = new CameraMovementController(transform);
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
            var movementVector = MovementVectorDelta();
            var zoomedVector = ZoomVectorDelta();
            Logging.Log(movementVector, zoomedVector);
            _rigidbody.MovePosition(_camera.transform.position + movementVector + zoomedVector);
        }
    }
}
