using Buildings.Workplace;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class DropdownStats : MonoBehaviour
    {
        protected VisualElement container;
        protected Button dropdown;
        protected Transform parent;

        protected List<Entity> items = new List<Entity>();
        protected GameObject chosenItem;
        protected bool isOpened;
        protected GameObject sender;

        protected Action itemRootAction;
        protected Action buttonClicked;
        
        private void Awake()
        {
            parent = new GameObject("Dropdown").transform;
            parent.parent = GameObject.Find($"Stats{FindObjectsOfType<Stats>().Length}").transform;
            gameObject.transform.parent = parent;

            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            dropdown = container.Q<Button>("Dropdown"); // tlačítka nejdou externě stylovat, ratio + barva se dá nastavit jenom u tlačítka a ne u DropdownContaineru, jinak nefunguje hover 

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

        public void SetDropdownItemButtonImage(string path)
        {
            foreach (GameObject item in parent.transform)
            {
                if (item.name.Contains("DropdownItem"))
                {
                    item.GetComponent<UIDocument>().rootVisualElement.Q<Button>("Button").style.backgroundImage = new StyleBackground(
                        Utils.LoadTexture(path));
                }
            }
        }

        protected void OnItemsChanged()
        {
            var arrow = container.Q<VisualElement>("Arrow");
            if (items.Count == 0)
            {
                arrow.visible = false;
                return;
            }

            arrow.visible = true;
        }

        protected void ReloadDropdownItems()
        {
            if (!isOpened) return;
            CloseDropdown();
            OpenDropdown();
        }

        public void SetOuterLabel(string text) => container.Q<Label>("LabelOut").text = text;

        public void SetInnerLabel(string text) => container.Q<Label>("LabelIn").text = text;

        public GameObject GetChosenItem() => chosenItem;

        private void OpenDropdown()
        {
            var dropdownItems = new List<VisualElement>();
            foreach (var entity in items)
            {
                var itemRoot = Instantiate(Resources.Load("Stats/DropdownItem") as GameObject,
                    parent).GetComponent<UIDocument>().rootVisualElement;
                itemRoot.Q<Label>().text = entity.GetName();
                try
                {
                    itemRootAction();
                }
                catch{}

                dropdownItems.Add(itemRoot);
                itemRoot.Q<Button>("DropdownItem").clicked += () =>
                {
                    chosenItem = entity.gameObject;
                    CloseDropdown();
                    isOpened = !isOpened;
                    //todo otevřít statistiky entity? (ne u focusu)
                };
                itemRoot.Q<Button>("Button").clicked += () => sender.GetComponent<Workplace>().FireWorker(entity);
            }

            var pos = dropdown.LocalToWorld(dropdown.contentRect);
            foreach (var item in dropdownItems)
            {
                pos.y += 16;
                item.style.top = pos.y;
                item.style.left = pos.x - 1;
            }
        }

        private void CloseDropdown()
        {
            foreach (var dropdownItem in gameObject.transform.parent.GetComponentsInChildren<Transform>()
                         .Where(x => x.name.Contains("DropdownItem")))
                Destroy(dropdownItem.gameObject);
        }
    }
}
