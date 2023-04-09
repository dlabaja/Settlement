using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class DropdownElement : MonoBehaviour
    {
        private VisualElement container;
        private Button dropdown;
        private Transform parent;

        protected List<GameObject> listObjects = new List<GameObject>();
        protected GameObject chosenItem;
        private bool isOpened;
        protected GameObject sender;

        protected Action buttonClicked;
        protected Action<GameObject> itemButtonClicked;
        protected Action<GameObject> onChoose;
        protected Action afterAwake;
        private StyleBackground itemImage;

        public void OnAwake(GameObject sender)
        {
            this.sender = sender;
            parent = transform.parent;

            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            dropdown = container.Q<Button>("Dropdown"); // tlačítka nejdou externě stylovat, ratio + barva se dá nastavit jenom u tlačítka a ne u DropdownContaineru, jinak nefunguje hover edit wtf ??? 

            dropdown.clicked += () =>
            {
                if (!isOpened)
                    OpenDropdown();
                else
                    CloseDropdown();
            };
            container.Q<Button>("Button").clicked += () => buttonClicked();
            afterAwake();
        }

        protected void SetDropdownButtonImage(string path) => container.Q<Button>("Button").style.backgroundImage = new StyleBackground(
            Utils.LoadTexture(path));

        protected void SetDropdownItemButtonImage(string path) => itemImage = new StyleBackground(Utils.LoadTexture(path));

        protected void OnItemsChanged()
        {
            var arrow = container.Q<VisualElement>("Arrow");
            if (listObjects.Count == 0)
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

        protected void SetOuterLabel(string text) => container.Q<Label>("LabelOut").text = text;

        protected void SetInnerLabel(string text) => container.Q<Label>("LabelIn").text = text;

        public GameObject GetChosenItem() => chosenItem;

        private void OpenDropdown()
        {
            var dropdownItems = new List<VisualElement>();
            foreach (var gm in listObjects)
            {
                var item = Instantiate(Resources.Load("UI/DropdownItem") as GameObject,
                    parent);
                item.GetComponent<UIDocument>().sortingOrder = Window.AddSortingOrder();
                item.name = $"DropdownItem {gameObject.GetHashCode()}";
                
                var itemRoot = item.GetComponent<UIDocument>().rootVisualElement;
                itemRoot.Q<Label>().text = gm.name;

                var itemDropdown = itemRoot.Q<Button>("DropdownItem");
                itemDropdown.clicked += () =>
                {
                    chosenItem = gm;
                    CloseDropdown();
                    isOpened = !isOpened;
                    onChoose?.Invoke(gm);
                    //todo otevřít statistiky entity? (ne u focusu)
                };
                dropdownItems.Add(itemRoot);

                var itemButton = itemRoot.Q<Button>("Button");
                itemButton.style.backgroundImage = itemImage;
                itemButton.clicked += () => itemButtonClicked?.Invoke(gm);
            }

            var pos = dropdown.LocalToWorld(dropdown.contentRect);
            foreach (var item in dropdownItems)
            {
                pos.y += 16;
                item.style.top = pos.y;
                item.style.left = pos.x - 1;
            }
            isOpened = !isOpened;
        }

        public void CloseDropdown()
        {
            foreach (var dropdownItem in transform.parent.GetComponentsInChildren<Transform>()
                         .Where(x => x.name == $"DropdownItem {gameObject.GetHashCode()}"))
                Destroy(dropdownItem.gameObject);
            isOpened = !isOpened;
        }
    }
}
