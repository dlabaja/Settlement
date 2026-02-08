using Constants;
using UnityEngine.InputSystem;

namespace Instances;

public static class InputActionMaps
{
    public static readonly InputActionMap Camera = InputSystem.actions.FindActionMap(InputActionMapName.Camera);
    public static readonly InputActionMap Mouse = InputSystem.actions.FindActionMap(InputActionMapName.Mouse);
}
