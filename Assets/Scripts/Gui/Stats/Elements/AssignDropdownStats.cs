using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class AssignDropdownStats : MonoBehaviour
    {
        private VisualElement container;
        private Button dropdown;
        private Transform parent;

        private List<GameObject> items = new List<GameObject>();
        private GameObject chosenItem;
        private bool isOpened;

        private void Awake()
        {
            parent = new GameObject("Dropdown").transform;
            parent.parent = GameObject.Find("Stats").transform;
            gameObject.transform.parent = parent;

            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            dropdown = container.Q<VisualElement>("DropdownContainer")
                .Q<VisualElement>().Q<Button>("Dropdown");
            dropdown.clicked += () =>
            {
                if (!isOpened)
                    OpenDropdown();
                else
                    CloseDropdown();
                isOpened = !isOpened;
            };
        }

        private void CloseDropdown()
        {
            foreach (var dropdownItem in gameObject.transform.parent.GetComponentsInChildren<Transform>()
                         .Where(x => x.name.Contains("DropdownItem")))
                Destroy(dropdownItem.gameObject);
        }

        private void OpenDropdown()
        {
            var dropdownItems = new List<VisualElement>();
            foreach (var item in items)
            {
                var itemRoot = Instantiate(Resources.Load("Stats/DropdownItem") as GameObject,
                    parent).GetComponent<UIDocument>().rootVisualElement.Children().FirstOrDefault();
                itemRoot.Q<Label>().text = item.name;
                dropdownItems.Add(itemRoot);
            }

            var pos = dropdown.LocalToWorld(dropdown.contentRect);
            foreach (var item in dropdownItems)
            {
                pos.y += 16;
                item.style.top = pos.y;
                item.style.left = pos.x - 1;
            }
        }

        public void AddItems(List<GameObject> items) => this.items = items;

        public void SetOuterLabel(string text) => container.Q<VisualElement>("LabelContainer")
            .Q<Label>().text = text;

        public void SetInnerLabel(string text) => dropdown.text = text;
    }
}
