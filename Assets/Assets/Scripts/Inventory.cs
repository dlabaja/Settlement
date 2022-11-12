using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Const;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int slots = 1;
        private Dictionary<Item, int> _inventory = new();

        public Dictionary<Item, int> GetInventory() => _inventory;

        public int GetItemCount(Item item) => _inventory.GetValueOrDefault(item, 0);

        private int GetMaxItemRoom(Item item) => GetFreeSlots() * 100 + _inventory.GetValueOrDefault(item, 0);

        private int GetFreeSlots()
        {
            var value = slots;
            foreach (var item in _inventory.Values)
            {
                value -= (int)Math.Ceiling((double)(item / 100));
            }

            return value;
        }

        public void TransferItems(GameObject gm, Item item, int count)
        {
            gm.GetComponent<Inventory>().RemoveItems(item, count);
            var maxitemy = GetMaxItemRoom(item);
            //receiver is full
            if (maxitemy == 0)
            {
                gm.GetComponent<Inventory>().AddItems(item, count);
            }
            //sender gives more than receiver can carry
            else if (count > maxitemy)
            {
                UpsertInventory(item, maxitemy);
                gm.GetComponent<Inventory>().AddItems(item, count - maxitemy);
            }
            else UpsertInventory(item, count);
        }

        public void AddItems(Item item, int count) => UpsertInventory(item, count);

        public void RemoveItems(Item item, int count) => _inventory[item] -= count;

        private void UpsertInventory(Item item, int count)
        {
            if (!_inventory.TryAdd(item, count))
            {
                _inventory[item] = count;
            }
        }

        /*
        public void GetAllItems(GameObject gm, Const.Items item)
        {
            var gameObject = GetComponent<CustomObject>();
            var other = gm.GetComponent<CustomObject>();
            gameObject.AddItem(item, other.TryGetItemCount(item));
            other.RemoveItem(item, other.TryGetItemCount(item));
        }
        
        public int TryGetItemCount(Const.Items item)
        {
            if (!inventory.ContainsKey(item)) return 0;

            return inventory[item];
        }

        public void AddItem(Const.Items item, int count)
        {
            GameController.UpdateGlobalInventory(new KeyValuePair<Const.Items, int>(item, count));
            if (!inventory.ContainsKey(item))
            {
                inventory.Add(item, count);
                return;
            }

            inventory[item] += count;
        }

        public void RemoveItem(Const.Items item, int count)
        {
            if (!inventory.ContainsKey(item)) return;

            inventory[item] -= count;
            GameController.UpdateGlobalInventory(new KeyValuePair<Const.Items, int>(item, -count));
        }*/
    }
}