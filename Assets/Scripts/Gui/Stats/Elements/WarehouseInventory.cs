using Buildings.Workplace;
using Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class WarehouseInventory : DropdownElement
    {
        private int index;
        private VisualElement root;

        public void OnStart(GameObject sender)
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            var inv = sender.GetComponent<Inventory.Inventory>();

            afterAwake = () =>
            {
                listObjects = new List<GameObject>{
                    ItemToGameObject(Const.Item.None), ItemToGameObject(Const.Item.Wood), ItemToGameObject(Const.Item.PolishedStone),
                    ItemToGameObject(Const.Item.Stone), ItemToGameObject(Const.Item.Tools), ItemToGameObject(Const.Item.Planks)
                };
            };
            onChoose = item =>
            {
                ReloadLabel();
                inv.ResetSlot(index, GameObjectToItem(item));
            };
            buttonClicked = () =>
            {
                inv.ResetSlot(index, Const.Item.None);
                SetInnerLabel("None (0)");
                chosenItem = ItemToGameObject(Const.Item.None);
            };

            SelectButton(root.Q<Button>("ButtonAllow"));
            foreach (var item in new[]{"ButtonAllow", "ButtonReject", "ButtonStockMax", "ButtonUnstock"})
            {
                var b = root.Q<Button>(item);
                b.clicked += () => SelectButton(b);
            }


            OnAwake(sender);
            InvokeRepeating(nameof(ReloadLabel), 0, 1);

        }

        private void SelectButton(Button button)
        {
            button.style.unityBackgroundImageTintColor = new Color(0f, 0f, 0f, 0.8f);
            button.style.backgroundColor = Color.white;
            button.parent.style.backgroundColor = Color.white;
            foreach (var item in root.Query<Button>().Where(x => x.parent.parent.name != "Container" && x != button).ToList())
            {
                item.style.unityBackgroundImageTintColor = Color.white;
                item.style.backgroundColor = new Color(0f, 0f, 0f, 0.8f);
                item.parent.style.backgroundColor = new Color(0f, 0f, 0f, 0.8f);
            }
        }

        private void ReloadLabel()
        {
            if (chosenItem is null)
            {
                SetInnerLabel("None (0)");
                return;
            }

            SetInnerLabel($"{chosenItem.name} ({sender.GetComponent<Inventory.Inventory>().GetInventory()[index].count})");
        }

        //kvůli dropdownu, který přijímá a vrací pouze gameobject a já jsem moc línej to přepisovat
        private GameObject ItemToGameObject(Const.Item item) => GameObject.Find("Items").transform.Find(item.ToString()).gameObject;
        private Const.Item GameObjectToItem(GameObject gm) => Enum.Parse<Const.Item>(gm.name);
        public void SetIndex(int index) => this.index = index;
    }
}
