using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public Dictionary<TKey, TValue> ToDictionary()
        {
            if (dictionary.Count == 0)
            {
                dictionary = new Dictionary<TKey, TValue>();
                for (int i = 0; i < keys.Count; i++)
                {
                    dictionary[keys[i]] = values[i];
                }
            }
            return dictionary;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ToDictionary().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public class Inventory2 : MonoBehaviour
    {
        [SerializeField] private int stackSize;
        [SerializeField] private int stackCount;
        [SerializeField] private List<Const.Item> whiteList = new List<Const.Item>();
        
        [SerializeField] private SerializableDictionary<Const.Item, int> startItems = new SerializableDictionary<Const.Item, int>();

        private Dictionary<Const.Item, int> inventory = new Dictionary<Const.Item, int>();

        private void Awake()
        {
            // initialize inventory
            foreach (Const.Item item in whiteList.Any() ? whiteList.ToArray() : Enum.GetValues(typeof(Const.Item)))
            {
                inventory.Add(item, 0);
            }
            
            // add start items
            foreach (var item in startItems)
            {
                inventory[item.Key] = item.Value;
            }
        }

        public void TransferItems(Const.Item item, int count, GameObject receiver)
        {
            var recInv = receiver.GetComponent<Inventory2>();
            var countToSend = recInv.inventory[item] + count > GetMaxItemRoom(item)
                ? count - (recInv.inventory[item] + count - GetMaxItemRoom(item))
                : count;

            countToSend = inventory[item] - countToSend < 0
                ? countToSend - (inventory[item] + countToSend) // countToSend musí být roven nule
                : countToSend;
            
            recInv.inventory[item] += countToSend;
            inventory[item] -= countToSend;
        }

        public void RemoveItem(Const.Item item, int count)
        {
            
        }

        public int GetMaxItemRoom(Const.Item item)
        {
            var occupiedStacks = 0;
            foreach (var i in inventory.Where(x => x.Value > 0 && x.Key != item))
            {
                occupiedStacks += (int)Math.Ceiling((double)i.Value / stackSize);
            }

            return (stackCount - occupiedStacks) * stackSize;
        }
    }
}
