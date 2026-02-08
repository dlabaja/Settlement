using UnityEngine.InputSystem;

namespace Models.Controls;

public class KeyControl
{
    public InputAction Action { get; }
    public bool IsPressed { get; private set; }
    public bool ToggleState { get; private set; }
        
    public KeyControl(InputAction action)
    {
        Action = action;
        Action.started += OnButtonPressed;
        Action.canceled += OnButtonReleased;
    }

    private void OnButtonReleased(InputAction.CallbackContext obj)
    {
        IsPressed = false;
    }

    private void OnButtonPressed(InputAction.CallbackContext obj)
    {
        IsPressed = true;
        ToggleState = !ToggleState;
    }

    public bool WasPressedThisFrame()
    {
        return Action.WasPressedThisFrame();
    }
}