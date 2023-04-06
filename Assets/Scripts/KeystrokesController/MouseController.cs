using Gui.Stats;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace KeystrokesController
{
    public class MouseController : KeystrokesController
    {
        private InputAction _mouseClick;
        public static bool LMBPressed;

        private void Start()
        {
            _mouseClick = _keystrokes.Mouse.Click;
            _mouseClick.performed += OnMouseClicked;
            _keystrokes.Mouse.Hold.started += _ => LMBPressed = true;
            _keystrokes.Mouse.Hold.canceled += _ => LMBPressed = false;
            
            _mouseClick.Enable();
            _keystrokes.Mouse.Hold.Enable(); //kdo to vymýšlel, trávil jsem nad tím hodinu
        }

        private void OnMouseClicked(InputAction.CallbackContext obj)
        {
            RenderStats();
        }

        private void RenderStats()
        {
            var pointer = Mouse.current.position.ReadValue();
            Ray ray = Camera.main!.ScreenPointToRay(pointer);
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = pointer;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);
            
            if (raycastResults.Count > 0) //ui is overlapping mouse click
                return;

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent<IStats>(out var stats) && Stats.statsEnabled)
                stats.GenerateStats();
        }
    }
}
