using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class DropdownStats : MonoBehaviour
    {
        private VisualElement container;
        private Button dropdown;
        
        private List<GameObject> items = new List<GameObject>();
        private GameObject chosenItem;
        private void Awake()
        {
            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            dropdown = container.Q<VisualElement>("DropdownContainer")
                .Q<VisualElement>().Q<Button>("Dropdown");
            dropdown.clicked += () => {}; //spawn dropdown itemů
        }

        public void AddItems(List<GameObject> items) => this.items = items;

        public void SetOuterLabel(string text) => container.Q<VisualElement>("LabelContainer")
            .Q<Label>().text = text;

        public void SetInnerLabel(string text) => dropdown.text = text;
    }
}
