using Gui.Stats.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui.Stats
{
    public class Window : MonoBehaviour
    {
        private const int elementOffset = 5;
        private const int offset = 20;

        protected VisualElement top;
        private VisualElement container;
        protected GameObject sender;
        protected Transform parent;

        public static int sortingOrder;

        public static Window GenerateWindow(GameObject sender)
        {
            if (IsDuplicate(sender)) return null;
            var obj = CustomObject.LoadGameObject("UI/Window", "Gui").GetComponent<Window>();
            obj.sender = sender;
            return obj;
        }

        protected static bool IsDuplicate(GameObject sender)
        {
            foreach (var item in FindObjectsOfType<Window>().Select(x => x.sender))
                if (item == sender)
                    return true;
            return false;
        }

        public static int AddSortingOrder()
        {
            sortingOrder += 5;
            return sortingOrder;
        }

        private void Awake()
        {
            transform.parent = new GameObject(name).transform;
            parent = gameObject.transform.parent;
            parent.parent = GameObject.Find("Gui").transform;
            GetComponent<UIDocument>().sortingOrder = AddSortingOrder();
            top = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Top");
            container = top.Q<VisualElement>("Container");
            top.Q<Button>("Close").visible = false;
        }

        public void BuildWindow()
        {
            container.schedule.Execute(() =>
            {
                var dp = 0f;
                var maxWidth = 0f;
                foreach (var child in container.Children().Select(x => x.Children().FirstOrDefault()))
                {
                    if (child == null)
                        continue;
            
                    child!.style.left = 0f;
                    child!.style.top = dp;
            
                    dp += child.layout.height + elementOffset;
                    if (child.layout.width > maxWidth)
                        maxWidth = child.layout.width;
                }
                
                var mousePos = Mouse.current.position.ReadValue();
                top.style.width = maxWidth + 2 * offset;
                top.style.height = dp + 2 * offset;
                top.style.top = Math.Clamp(Screen.height - mousePos.y - top.style.height.value.value - 20, 0, Screen.height);
                top.style.left = Math.Clamp(mousePos.x - 0.5f * top.style.width.value.value, 0, Screen.width);
            });
        }

        private UIDocument AddToContainer(string name)
        {
            var obj = Instantiate(Resources.Load($"UI/{name}") as GameObject, gameObject.transform.parent).GetComponent<UIDocument>();
            container.Add(obj.rootVisualElement);
            obj.transform.parent = transform.parent;
            return obj;
        }

        public Window AddLabel(string text, int fontSize = 14)
        {
            var root = AddToContainer("Label").rootVisualElement;
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = text;
            root.Q<Label>().style.fontSize = fontSize;
            return this;
        }

        public Window AddLabel(Func<string> getText, int fontSize = 14)
        {
            var item = AddToContainer("Label");
            var root = item.rootVisualElement;
            root = root.Q<VisualElement>("Container");
            var textLabel = root.Q<Label>();
            textLabel.style.fontSize = fontSize;
            
            textLabel.text = getText();
            item.GetComponent<LabelElement>().OnLabelResized(null);
            root.schedule.Execute(() =>
            {
                textLabel.text = getText();
            }).Every(1000);
            root.Q<Label>().style.fontSize = fontSize;
            return this;
        }

        public Window AddLabelWithText(string label, string text, int fontSize = 14)
        {
            var item = AddToContainer("LabelWithText");
            var root = item.rootVisualElement;
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = label;
            root.Q<Label>().style.fontSize = fontSize;

            var textLabel = root.Q<Label>("Text");
            textLabel.style.fontSize = fontSize;
            textLabel.text = text;
            return this;
        }

        public Window AddLabelWithText(string label, Func<string> getText, int fontSize = 14)
        {
            var item = AddToContainer("LabelWithText");
            var root = item.rootVisualElement;
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = label;
            root.Q<Label>().style.fontSize = fontSize;

            var textLabel = root.Q<Label>("Text");
            textLabel.style.fontSize = fontSize;
            root.schedule.Execute(() =>
            {
                textLabel.text = getText();
            }).Every(1000);
            return this;
        }

        public Window AddSpace()
        {
            AddToContainer("Space");
            return this;
        }

        public Window AddButton(string text)
        {
            AddToContainer("Button").GetComponent<UIDocument>().rootVisualElement.Q<Button>().text = text;
            return this;
        }
        
        public Window AddWarehouseInventory()
        {
            for (int i = 0; i < 4; i++)
            {
                var inv = AddToContainer("WarehouseInventory").GetComponent<WarehouseInventory>();
                inv.OnStart(sender, i);
            }
            return this;
        }

        public Window AddAssignDropdown()
        {
            var dropdown = AddToContainer("DropdownAssign").GetComponent<AssignDropdownElement>();
            dropdown.OnAwake(sender);
            return this;
        }

        public Window AddFocusDropdown(List<GameObject> items, string outerLabel)
        {
            var dropdown = AddToContainer("DropdownFocus").GetComponent<FocusDropdownElement>();
            dropdown.SetupDropdown(items, outerLabel);
            dropdown.OnAwake(sender);
            return this;
        }
    }
}
