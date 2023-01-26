using UnityEngine;
using UnityEngine.InputSystem;

namespace KeystrokesController
{
    public class KeystrokesController : MonoBehaviour
    {
        protected static Keystrokes _keystrokes;
        private bool _rightClickPressed;
        protected Rigidbody _rigidbody;
        private CameraController _camera;
        private MouseController _mouse;

        private void Awake()
        {
            _keystrokes = new Keystrokes();
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _keystrokes.Camera.Drag.performed += OnRightClick;
            _keystrokes.Camera.Drag.canceled += OnNotRightClick;
            
            _camera = GetComponent<CameraController>();
            _mouse = GetComponent<MouseController>();
        }

        private void FixedUpdate()
        {
            _camera.CameraMovement();
        }

        private void LateUpdate()
        {
            _camera.CameraZoom();
            if (_rightClickPressed)
                _camera.CameraDrag();
        }

        public static void DisableInput() => _keystrokes.Disable();
        public static void EnableInput() => _keystrokes.Enable();

        private void OnNotRightClick(InputAction.CallbackContext obj) => _rightClickPressed = false;
        private void OnRightClick(InputAction.CallbackContext obj) => _rightClickPressed = true;
    }
}