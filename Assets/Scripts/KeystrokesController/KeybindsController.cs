using Gui.Stats;

namespace KeystrokesController
{
    public class KeybindsController : KeystrokesController
    {
        private void Start()
        {
            _keystrokes.Keybinds.CloseStats.performed += _ => Stats.CloseAllStats();
            _keystrokes.Keybinds.CloseStats.Enable();
        }
    }
}
