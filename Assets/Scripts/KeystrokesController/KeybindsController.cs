using Gui.Stats;
using UnityEngine;

namespace KeystrokesController
{
    public class KeybindsController : KeystrokesController
    {
        private void Start()
        {
            _keystrokes.Keybinds.CloseStats.performed += delegate
            {
                foreach (Transform item in GameObject.Find("Gui").transform)
                    if (item.name.Contains("Stats"))
                        Destroy(item.gameObject);
            };

            _keystrokes.Keybinds.CloseStats.Enable();
        }
    }
}
