using Gui.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeystrokesController
{
    public class MouseController : KeystrokesController
    {
        private InputAction _mouseClick;
        private void Start()
        {
            _mouseClick = _keystrokes.Mouse.Click;
            _keystrokes.Mouse.Click.performed += OnMouseClicked;
            _mouseClick.Enable();
        }

        private void OnMouseClicked(InputAction.CallbackContext obj)
        {
            ShootRay();
        }

        private void ShootRay()
        {
            Ray ray = Camera.main!.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Stats.GenerateStats(hit.collider.gameObject);
            }
            //todo pokud je to terén/nezařazeno zavři všechny okna
        }
    }
}
