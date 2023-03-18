using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Const;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int slots = 1;
        [SerializeField] private int stackSize = 100;
        private Dictionary<int, ItemStruct> _inventory = new();
        //todo inspector debug
        [SerializeField] public List<ItemStruct> _startValues = new();
        [SerializeField] private bool itemsAreConstant;

        //todo inspector debug
        private void Awake()
        {
            if (itemsAreConstant)
            {
                slots = _startValues.Count;
                for (int i = 0; i < slots; i++)
                    _inventory.Add(i, _startValues[i]);
            }
            else
            {
                for (int i = 0; i < slots; i++)
                    _inventory.Add(i, new ItemStruct(Item.None, 0));
            }

            for (int i = 0; i < _startValues.Count; i++)
                AddItems(_startValues[i].item, _startValues[i].count);
        }

        //TODO only temporary for debug, add to stats
        [SerializeField] private List<ItemStruct> _values;

        //todo temporary
        private void FixedUpdate()
        {
            _values = _inventory.Values.ToList();
        }

        public Dictionary<int, ItemStruct> GetInventory() => _inventory;

        public int AddItems(Item item, int count)
        {
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].item == item || _inventory[i].item == Item.None)
                    while (_inventory[i].count < stackSize && count > 0)
                    {
                        _inventory[i] = new ItemStruct(item, _inventory[i].count + 1);
                        count--;
                    }

                if (count == 0)
                    break;
            }

            return count;
        }

        public int GetItemCount(Item item)
        {
            var val = 0;
            foreach (var i in _inventory.Values)
            {
                if (i.item == item)
                    val += i.count;
            }

            return val;
        }

        private int RemoveItems(Item item, int count)
        {
            var defaultCount = count;
            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i].item == item)
                    while (_inventory[i].count > 0 && count > 0)
                    {
                        _inventory[i] = new ItemStruct(item, _inventory[i].count - 1);
                        count--;
                    }

                if (count == 0)
                    break;
            }

            return defaultCount - count;
        }

        private void ReplaceWithNone()
        {
            for (var i = 0; i < _inventory.Count; i++)
                if (_inventory[i].count == 0)
                    _inventory[i] = new ItemStruct(Item.None, 0);
        }

        private void ReplaceWithStartValues()
        {
            for (var i = 0; i < _inventory.Count; i++)
                if (_inventory[i].item == Item.None)
                    _inventory[i] = new ItemStruct(_startValues[i].item, 0);
        }

        public int CountAllItems()
        {
            var val = 0;
            foreach (var item in _inventory.Values)
            {
                if (item.item == Item.None) continue;
                val += item.count;
            }

            return val;
        }

        public bool IsFull()
        {
            return slots * stackSize == CountAllItems();
        }

        public bool IsEmpty() => CountAllItems() == 0;

        public bool TransferItems(Item item, int count, GameObject receiver = null, GameObject sender = null)
        {
            if (sender == null) sender = gameObject;
            if (receiver == null) receiver = gameObject;

            var senderInv = sender.GetComponent<Inventory>();
            var receiverInv = receiver.GetComponent<Inventory>();

            //přidá do svého inventáře zbytek po přidávání do cizího inventáře nebo tak nějak
            if (receiverInv.IsFull()) return false;

            var removed = senderInv.RemoveItems(item, count);
            senderInv.AddItems(item, receiverInv.AddItems(item, removed));

            if (!itemsAreConstant)
            {
                senderInv.ReplaceWithNone();
                receiverInv.ReplaceWithNone();
            }
            else
                receiverInv.ReplaceWithStartValues();

            return true;
        }
    }

    [Serializable]
    public struct ItemStruct
    {
        public ItemStruct(Item item, int count)
        {
            this.item = item;
            this.count = count;
        }

        public Item item;
        public int count;

        public override string ToString() => $"({item}, {count})";
    }
}
