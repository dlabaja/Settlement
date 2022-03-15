using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Keystrokes
{
    public class KeystrokesController : MonoBehaviour
    {
        [SerializeField] private float cameraDragSpeed = 2;
        [SerializeField] private float cameraSpeed = .25f;
        private InputAction _cameraDrag;
        private InputAction _cameraMovement;
        private global::Keystrokes _keystrokes;
        private bool _rightClickPressed;

        private void Awake()
        {
            _keystrokes = new global::Keystrokes();
        }

        private void LateUpdate()
        {
            /*var z = Mouse.current.scroll.ReadValue<float>();
            if (z > 0)
                Debug.Log("Scroll UP");
            else if (z < 0)
                Debug.Log("Scroll DOWN");*/

            var movement = _cameraMovement.ReadValue<Vector3>() * cameraSpeed;
            var cameraLookingDirection = transform.rotation * Vector3.forward;
            cameraLookingDirection = new Vector3(cameraLookingDirection.x, 0, cameraLookingDirection.z).normalized;
            var absuluteMovement = Quaternion.FromToRotation(Vector3.forward, cameraLookingDirection) * movement;
            transform.position += absuluteMovement;

            if (_rightClickPressed)
            {
                var x = Mouse.current.delta.x.ReadValue() * cameraDragSpeed;
                var y = Mouse.current.delta.y.ReadValue() * cameraDragSpeed;

                if (Mathf.Abs(Utils.WrapAngle(transform.eulerAngles.x - y)) > 80) y = 0;
                transform.eulerAngles += new Vector3(-y, x, 0);
            }
        }

        private void OnEnable()
        {
            EnableCamera();
        }

        private void OnDisable()
        {
            DisableCamera();
        }

        private void EnableCamera()
        {
            _cameraMovement = _keystrokes.Camera.Movement;
            _cameraMovement.Enable();

            _cameraDrag = _keystrokes.Camera.Drag;
            _keystrokes.Camera.Drag.performed += OnRightClick;
            _keystrokes.Camera.Drag.canceled += OnNotRightClick;
            _cameraDrag.Enable();
        }

        private void OnNotRightClick(InputAction.CallbackContext obj)
        {
            _rightClickPressed = false;
        }

        private void OnRightClick(InputAction.CallbackContext obj)
        {
            _rightClickPressed = true;
        }

        private void DisableCamera()
        {
            _cameraMovement.Disable();

            _cameraDrag.performed -= OnRightClick;
            _cameraDrag.Disable();
        }
    }
}