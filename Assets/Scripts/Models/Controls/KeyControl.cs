using UnityEngine.InputSystem;

namespace Models.Controls
{
    public class KeyControl
    {
        private InputAction _action;
        public bool IsPressed { get; private set; }
        
        public KeyControl(InputAction action)
        {
            _action = action;
            _action.started += OnButtonPressed;
            _action.canceled += OnButtonReleased;
        }

        private void OnButtonReleased(InputAction.CallbackContext obj)
        {
            IsPressed = false;
        }

        private void OnButtonPressed(InputAction.CallbackContext obj)
        {
            IsPressed = true;
        }

        public bool WasPressedThisFrame()
        {
            return _action.WasPressedThisFrame();
        }
    }
}
