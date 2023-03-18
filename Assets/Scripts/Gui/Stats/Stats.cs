using Gui.Stats.Elements;
using KeystrokesController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui.Stats
{
    public class Stats : MonoBehaviour
    {
        private const int elementOffset = 5;
        private const int offset = 20;

        private VisualElement root;
        private VisualElement container;
        private GameObject sender;
        private Transform parent;

        public static int sortingOrder;

        public static Stats GenerateStats(GameObject sender)
        {
            var name = $"Stats {sender.GetHashCode()}";
            sortingOrder++;
            if (GameObject.Find(name) == null)
            {
                var obj = Utils.LoadGameObject("Stats/Stats", Const.Parent.Gui).GetComponent<Stats>();
                obj.sender = sender;
                Mouse.current.position.ReadValue();
                obj.transform.parent = new GameObject(name).transform;
                obj.parent = obj.transform.parent;
                obj.parent.parent = GameObject.Find("Gui").transform;
                return obj;
            }

            return null;
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            sortingOrder++;
            gameObject.GetComponent<UIDocument>().sortingOrder = sortingOrder;
            StartCoroutine(DragWindow());

            foreach (Transform item in parent.transform)
                if (item.TryGetComponent(typeof(DropdownStats), out var comp))
                    ((DropdownStats)comp).CloseDropdown();
        }

        private void Awake()
        {
            GetComponent<UIDocument>().sortingOrder = sortingOrder;
            root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Top");
            root.RegisterCallback<PointerDownEvent>(OnPointerDown);

            container = root.Q<VisualElement>("Container");

            var close = root.Q<Button>("Close");
            close.clicked += () => CloseButton.Close(parent.gameObject);
        }

        private IEnumerator DragWindow()
        {
            while (MouseController.RMBPressed)
            {
                var mousePos = Mouse.current.delta.ReadValue();
                root.style.top = Math.Clamp(root.style.top.value.value - mousePos.y, 0, Screen.height - root.layout.height);
                root.style.left = Math.Clamp(root.style.left.value.value + mousePos.x, 0, Screen.width - root.layout.width);
                yield return new WaitForEndOfFrame();
            }
        }

        public void BuildStats()
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
                root.style.width = maxWidth + 2 * offset;
                root.style.height = dp + 2 * offset;
                root.style.top = Math.Clamp(Screen.height - mousePos.y - root.style.height.value.value - 20, 0, Screen.height);
                root.style.left = Math.Clamp(mousePos.x - 0.5f * root.style.width.value.value, 0, Screen.width);
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
        
        public Stats AddLabel(Func<string> getText, int fontSize = 14)
        {
            var root = AddToContainer("Label").rootVisualElement;
            root = root.Q<VisualElement>("Container");
            var textLabel = root.Q<Label>();
            textLabel.style.fontSize = fontSize;
            root.schedule.Execute(() =>
            {
                textLabel.text = getText();
            }).Every(1000);
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
        
        public Stats AddLabelWithText(string label, Func<string> getText, int fontSize = 14)
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
        
        public Stats AddLabelWithTextVertical(string label, Func<string> getText, int fontSize = 14)
        {
            var item = AddToContainer("LabelWithTextVertical");
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
    }
}
