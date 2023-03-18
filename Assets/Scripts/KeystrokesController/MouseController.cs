using Gui.Stats;
using Interfaces;
using System;
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
        public static bool RMBPressed;

        private void Start()
        {
            _mouseClick = _keystrokes.Mouse.Click;
            _mouseClick.performed += OnMouseClicked;
            _keystrokes.Mouse.Hold.started += _ => RMBPressed = true;
            _keystrokes.Mouse.Hold.canceled += _ => RMBPressed = false;
            _mouseClick.Enable();
            _keystrokes.Mouse.Hold.Enable(); //kdo to vymýšlel, trávil jsem nad tím hodinu
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

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent<IStats>(out var stats))
                stats.GenerateStats();
        }
    }
}
