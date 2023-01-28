using Gui.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
            var pointer = Mouse.current.position.ReadValue();
            Ray ray = Camera.main!.ScreenPointToRay(pointer);
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = pointer;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if (raycastResults.Count > 0) return;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Stats.GenerateStats(hit.collider.gameObject);
            }
        }
    }
}
