using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Const;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int slots = 1;
        private Dictionary<Item, int> _inventory = new();
        [SerializeField] private List<Item> _itemy = new();
        [SerializeField] private List<int> _hodnoty = new();

        //TODO loads items from inspector at start, also temporary
        private void Awake()
        {
            for (int i = 0; i < _itemy.Count; i++)
            {
                _inventory.Add(_itemy[i], _hodnoty[i]);
            }
        }

        //TODO only temporary for debug
        [SerializeField] private string iinventory;

        private void FixedUpdate()
        {
            var str = "";
            foreach (var i in _inventory)
            {
                str += ($"{i.Key}:{i.Value}\n");
            }

            iinventory = str;
        }

        public Dictionary<Item, int> GetInventory() => _inventory;

        //current number of item in inventory
        public int GetItemCount(Item item) => _inventory.GetValueOrDefault(item, 0);

        //returns max space for specific item
        private int GetMaxItemRoom(Item item) => GetFreeSlots() * 100 + _inventory.GetValueOrDefault(item, 0);

        //returns slot unoccupied with items
        private int GetFreeSlots()
        {
            var value = slots;
            foreach (var item in _inventory.Values)
            {
                value -= (int)Math.Ceiling((double)(item / 100));
            }

            return value;
        }

        //transfers items from sender (gameobject) to receiver
        public void TransferItems(GameObject receiver, Item item, int count)
        {
            var senderInv = gameObject.GetComponent<Inventory>();
            var receiverInv = receiver.GetComponent<Inventory>();
            var maxitems = receiverInv.GetMaxItemRoom(item);
            if (!senderInv.RemoveItems(item, count)) return;

            //receiver is full
            if (maxitems == 0)
            {
                senderInv.AddItems(item, count);
            }
            //sender gives more than receiver can carry
            else if (count > maxitems)
            {
                receiverInv.UpsertInventory(item, maxitems);
                senderInv.AddItems(item, count - maxitems);
            }
            else receiverInv.UpsertInventory(item, count);
        }

        //adds and removes items, use TransferItems instead
        private void AddItems(Item item, int count) => UpsertInventory(item, count);

        private bool RemoveItems(Item item, int count)
        {
            if (!_inventory.ContainsKey(item)) return false;
            _inventory[item] -= count;
            if (_inventory[item] <= 0) _inventory.Remove(item);
            return true;
        }

        //adds items to stack or creates new slot for them
        private void UpsertInventory(Item item, int count)
        {
            if (!_inventory.TryAdd(item, count))
            {
                _inventory[item] = count;
            }
        }
    }
}