using Buildings.Workplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class AssignDropdownStats : MonoBehaviour
    {
        private VisualElement container;
        private VisualElement dropdownContainer;
        private Button dropdown;
        private Transform parent;

        private List<Entity> items = new List<Entity>();
        private GameObject chosenItem;
        private bool isOpened;
        private GameObject sender;

        private void Awake()
        {
            parent = new GameObject("Dropdown").transform;
            parent.parent = GameObject.Find($"Stats{FindObjectsOfType<Stats>().Length}").transform;
            gameObject.transform.parent = parent;

            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            dropdownContainer = container.Q<VisualElement>("DropdownContainer");
            dropdown = dropdownContainer.Q<VisualElement>().Q<Button>("Dropdown");

            dropdown.clicked += () =>
            {
                if (!isOpened)
                    OpenDropdown();
                else
                    CloseDropdown();
                isOpened = !isOpened;
            };
        }

        public void SetupSender(GameObject senderObj)
        {
            sender = senderObj;
            var workplace = sender.GetComponent<Workplace>();
            items = workplace.GetWorkers();
            OnItemsChanged();

            workplace.OnWorkersChanged += () =>
            {
                items = workplace.GetWorkers();
                SetInnerLabel($"Workers: {items.Count}/{workplace.GetMaxWorkers()}");
                OnItemsChanged();
                ReloadDropdownItems();
            };

            dropdownContainer.Q<VisualElement>("ButtonContainer").Q<Button>().clicked += () =>
            {
                var worker = FindObjectsOfType<Entity>()
                    .FirstOrDefault(x => x.Workplace.name == Const.CustomObjects.Spawn.ToString());
                workplace.AssignWorker(worker);
            };
            SetOuterLabel("Workers");
            SetInnerLabel($"Workers: {items.Count}/{workplace.GetMaxWorkers()}");
        }

        private void OnItemsChanged()
        {
            var arrow = dropdownContainer.Q<VisualElement>("Arrow");
            if (items.Count == 0)
            {
                arrow.visible = false;
                return;
            }
            arrow.visible = true;
        }

        private void ReloadDropdownItems()
        {
            if (!isOpened) return;
            CloseDropdown();
            OpenDropdown();
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
            foreach (var entity in items)
            {
                var itemRoot = Instantiate(Resources.Load("Stats/DropdownItem") as GameObject,
                    parent).GetComponent<UIDocument>().rootVisualElement;
                itemRoot.Q<Label>().text = entity.GetName();
                dropdownItems.Add(itemRoot);

                itemRoot.Q<Button>("DropdownItem").clicked += () =>
                {
                    chosenItem = entity.gameObject;
                    CloseDropdown();
                    isOpened = !isOpened;
                };

                itemRoot.Q<Button>("Button").clicked += () =>
                {
                    sender.GetComponent<Workplace>().FireWorker(entity);
                };
            }

            var pos = dropdown.LocalToWorld(dropdown.contentRect);
            foreach (var item in dropdownItems)
            {
                pos.y += 16;
                item.style.top = pos.y;
                item.style.left = pos.x - 1;
            }
        }

        public void SetOuterLabel(string text) => container.Q<VisualElement>("LabelContainer")
            .Q<Label>().text = text;

        public void SetInnerLabel(string text) => dropdown.text = text;

        public GameObject GetChosenItem() => chosenItem;
    }
}
