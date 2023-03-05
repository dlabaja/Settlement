using Buildings;
using Buildings.Workplace;
using Gui.Stats.Elements;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui.Stats
{
    public class Stats : MonoBehaviour
    {
        private const int elementOffset = 5;
        private const int offset = 20;
        
        private VisualElement container;

        public static Stats GenerateStats() => Utils.LoadGameObject("Stats/Stats", Const.Parent.Gui).GetComponent<Stats>();

        private void Awake()
        {
            var parent = new GameObject($"Stats{FindObjectsOfType<Stats>().Length}");
            parent.transform.parent = GameObject.Find("Gui").transform;
            gameObject.transform.parent = parent.transform;

            var root = GetComponent<UIDocument>().rootVisualElement;
            container = root.Q<VisualElement>("Container");
            var close = root.Q<Button>("Close");
            close.clicked += () => CloseButton.Close(parent);
        }

        public void BuildStats() //trvalo asi 3 hodiny, odkazoval jsem na template místo elementu 
        {
            container.schedule.Execute(() =>
            {
                var dp = container.style.top.value.value;
                var maxWidth = 0f;
                foreach (var child in container.Children().Select(x => x.Children().FirstOrDefault()))
                {
                    child!.style.top = dp;
                    dp += child.layout.height + elementOffset;
                    if (child.layout.width > maxWidth)
                        maxWidth = child.layout.width;
                }
                var root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Top");
                root.style.width = maxWidth;
                root.style.height = dp + 2 * offset;
            });
        }

        private UIDocument AddToContainer(string name)
        {
            var obj = Instantiate(Resources.Load($"Stats/{name}") as GameObject, gameObject.transform.parent).GetComponent<UIDocument>();
            container.Add(obj.rootVisualElement);
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
        
        public Stats AddAssignDropdown(GameObject sender)
        {
            var dropdown = AddToContainer("DropdownAssign").GetComponent<AssignDropdownStats>();
            dropdown.SetupSender(sender);
            return this;
        }
        
        public Stats AddFocusDropdown(List<GameObject> items)
        {
            AddToContainer("DropdownFocus");
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
