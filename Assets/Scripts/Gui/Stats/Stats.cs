using Gui.Stats.Elements;
using KeystrokesController;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui.Stats
{
    public class Stats : Window
    {
        public static bool statsEnabled = true;
        public static Stats GenerateStats(GameObject sender)
        {
            if (IsDuplicate(sender)) return null;
            var obj = Utils.LoadGameObject("UI/Stats", Const.Parent.Gui).GetComponent<Stats>();
            obj.sender = sender;
            return obj;
        }
        
        private void Start()
        {
            top.RegisterCallback<PointerDownEvent>(OnPointerDown);
            var close = top.Q<Button>("Close");
            close.visible = true;
            close.clicked += () => CloseButton.Close(parent.gameObject);
        }
        
        public static void CloseAllStats()
        {
            foreach (Transform item in GameObject.Find("Gui").transform)
                if (item.name.Contains("Stats"))
                    Destroy(item.gameObject);
        }
        
        private void OnPointerDown(PointerDownEvent evt)
        {
            gameObject.GetComponent<UIDocument>().sortingOrder = AddSortingOrder();
            StartCoroutine(DragWindow());

            foreach (Transform item in parent.transform)
                if (item.TryGetComponent(typeof(DropdownElement), out var comp))
                    ((DropdownElement)comp).CloseDropdown();
        }
        
        private IEnumerator DragWindow()
        {
            while (MouseController.LMBPressed)
            {
                var mousePos = Mouse.current.delta.ReadValue();
                top.style.top = Math.Clamp(top.style.top.value.value - mousePos.y, 0, Screen.height - top.layout.height);
                top.style.left = Math.Clamp(top.style.left.value.value + mousePos.x, 0, Screen.width - top.layout.width);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
