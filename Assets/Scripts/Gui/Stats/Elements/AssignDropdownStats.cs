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
            dropdown = container.Q<Button>("Dropdown"); // tlačítka nejdou externě stylovat, ratio + barva se dá nastavit jenom u tlačítka a ne u DropdownContaineru, jinak nefunguje hover 
            SetDropdownButtonImage("Assets/Sprites/assign.png");

            dropdown.clicked += () =>
            {
                if (!isOpened)
                    OpenDropdown();
                else
                    CloseDropdown();
                isOpened = !isOpened;
            };
        }

        public void SetDropdownButtonImage(string path) => container.Q<Button>("Button").style.backgroundImage = new StyleBackground(
            Utils.LoadTexture(path));

        public void SetDropdownItemButtonImage(VisualElement item, string path) => item.Q<Button>("Button").style.backgroundImage = new StyleBackground(
            Utils.LoadTexture(path));

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

            container.Q<VisualElement>("ButtonContainer").Q<Button>().clicked += () =>
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
            var arrow = container.Q<VisualElement>("Arrow");
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
                SetDropdownItemButtonImage(itemRoot, "Assets/Sprites/unassign.png");

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

        public void SetOuterLabel(string text) => container.Q<Label>("LabelOut").text = text;

        public void SetInnerLabel(string text) => container.Q<Label>("LabelIn").text = text;

        public GameObject GetChosenItem() => chosenItem;
    }
}
