using Buildings;
using Buildings.Workplace;
using Gui.Stats.Elements;
using Interfaces;
using KeystrokesController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui.Stats
{
    public class Stats : MonoBehaviour
    {
        private const int elementOffset = 5;
        private const int offset = 20;
        private Vector2 pos = new Vector2();

        private VisualElement root;
        private VisualElement container;
        private Button button;
        private GameObject sender;
        private Transform parent;

        private static int sortingOrder;
        private bool isDragging;

        public static Stats GenerateStats(GameObject sender)
        {
            var name = $"Stats {sender.GetHashCode()}";
            sortingOrder++;
            if (GameObject.Find(name) == null)
            {
                var obj = Utils.LoadGameObject("Stats/Stats", Const.Parent.Gui).GetComponent<Stats>();
                obj.sender = sender;
                obj.pos = Mouse.current.position.ReadValue();
                obj.transform.parent = new GameObject(name).transform;
                obj.parent = obj.transform.parent;
                obj.parent.parent = GameObject.Find("Gui").transform;
                return obj;
            }

            return null;
        }

        private void Awake()
        {
            GetComponent<UIDocument>().sortingOrder = sortingOrder;
            root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Top");
            container = root.Q<VisualElement>("Container");
            var close = root.Q<Button>("Close");
            close.clicked += () => CloseButton.Close(parent.gameObject);

            button = root.Q<Button>("Button");
            button.RegisterCallback<PointerMoveEvent>(delegate
            {
                var mousePos = Mouse.current.position.ReadValue();
                if (MouseController.isPressed)
                {
                    root.style.top = Math.Clamp(Screen.height - mousePos.y - 10, 0, Screen.height - root.layout.height);
                    root.style.left = Math.Clamp(mousePos.x - 0.5f * root.layout.width, 0, Screen.width - root.layout.width);
                }
            });
            button.clicked += () =>
            {
                sortingOrder++;
                GetComponent<UIDocument>().sortingOrder = sortingOrder;
            };
        }

        public void BuildStats()
        {
            container.schedule.Execute(() =>
            {
                var dp = 0f;
                var maxWidth = 0f;
                foreach (var child in container.Children().Select(x => x.Children().FirstOrDefault()))
                {
                    // Set the position of each child element relative to the container
                    child!.style.left = 0f;
                    child!.style.top = dp;

                    dp += child.layout.height + elementOffset;
                    if (child.layout.width > maxWidth)
                        maxWidth = child.layout.width;
                }

                var mousePos = Mouse.current.position.ReadValue();
                root.style.width = maxWidth + 2 * offset;
                root.style.height = dp + 2 * offset;
                root.style.top = Screen.height - mousePos.y - root.style.height.value.value - 20;
                root.style.left = mousePos.x - 0.5f * root.style.width.value.value;
            });

        }

        private UIDocument AddToContainer(string name)
        {
            var obj = Instantiate(Resources.Load($"Stats/{name}") as GameObject, gameObject.transform.parent).GetComponent<UIDocument>();
            container.Add(obj.rootVisualElement);
            obj.transform.parent = transform.parent;
            return obj;
        }

        public Stats AddLabel(string text, int fontSize = 14)
        {
            var root = AddToContainer("Label").rootVisualElement;
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = text;
            root.Q<Label>().style.fontSize = fontSize;
            return this;
        }

        public Stats AddSpace()
        {
            AddToContainer("Space");
            return this;
        }

        public Stats AddAssignDropdown()
        {
            var dropdown = AddToContainer("DropdownAssign").GetComponent<AssignDropdownStats>();
            dropdown.OnAwake(sender);
            return this;
        }

        public Stats AddFocusDropdown(List<GameObject> items, string outerLabel)
        {
            var dropdown = AddToContainer("DropdownFocus").GetComponent<FocusDropdownStats>();
            dropdown.SetupDropdown(items, outerLabel);
            dropdown.OnAwake(sender);
            return this;
        }

        public Stats AddLabelWithText(string label, string text, int fontSize = 14)
        {
            var root = AddToContainer("LabelWithText").rootVisualElement;
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = label;
            root.Q<Label>().style.fontSize = fontSize;

            root.Q<Label>("Text").text = text;
            root.Q<Label>("Text").style.fontSize = fontSize;
            return this;
        }
    }
}
