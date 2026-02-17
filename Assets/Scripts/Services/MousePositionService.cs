using UnityEngine;
using UnityEngine.InputSystem;

namespace Services;

public class MousePositionService
{
    public InputAction PositionAction { get; }
    public InputAction DeltaAction { get; }
    
    public MousePositionService(InputAction positionAction, InputAction deltaAction)
    {
        PositionAction = positionAction;
        DeltaAction = deltaAction;
    }

    public Vector2 Position
    {
        get => PositionAction.ReadValue<Vector2>();
    }

    public Vector2 Delta
    {
        get => DeltaAction.ReadValue<Vector2>();
    }
}
