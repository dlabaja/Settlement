using Buildings;
using Buildings.Workplace;
using Gui.Stats.Elements;
using Interfaces;
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
        private VisualElement container;
        private const int elementOffset = 5;
        private const int offset = 20;

        public static Stats GenerateStats() => Utils.LoadGameObject("Stats/Stats", Const.Parent.Gui).GetComponent<Stats>();

        private void Awake()
        {
            var parent = new GameObject("Stats");
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
                    print(dp);
                    dp += child.layout.height + elementOffset;
                    if (child.layout.width > maxWidth)
                        maxWidth = child.layout.width;
                }
                
                var root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Top");
                root.style.width = maxWidth + 2 * offset;
                root.style.height = dp + 2 * offset;
            });
        }

        private VisualElement AddToContainer(StatsElements name)
        {
            var obj = Instantiate(Resources.Load($"Stats/{name}") as GameObject, gameObject.transform.parent).GetComponent<UIDocument>().rootVisualElement;
            container.Add(obj);
            return obj;
        }

        public Stats AddLabel(string text)
        {
            var root = AddToContainer(StatsElements.Label);
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = text;
            return this;
        }

        public Stats AddLabelWithText(string label, string text)
        {
            var root = AddToContainer(StatsElements.LabelWithText);
            root = root.Q<VisualElement>("Container");
            root.Q<Label>().text = label;
            root.Q<Label>("Text").text = text;
            return this;
        }

        public enum StatsElements
        {
            Label,
            LabelWithText
        }
    }
}
