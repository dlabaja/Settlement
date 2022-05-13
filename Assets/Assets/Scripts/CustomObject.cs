using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class CustomObject : MonoBehaviour
    {
        [SerializeField] private int itemSlotLimit = 1;
        [SerializeField] private List<string> _inventory;
        private readonly Dictionary<Const.Items, int> inventory = new();

        private void FixedUpdate()
        {
            _inventory = new List<string>();
            foreach (var item in inventory) _inventory.Add(item.Key + ";" + item.Value);
        }

        private void OnDestroy()
        {
            try
            {
                foreach (var entity in FindObjectsOfType<Entity>()) entity.RemoveFromLookingFor(gameObject);
            }
            catch (Exception ignored)
            {
                // ignored
            }
        }

        public static void Spawn<T>()
        {
            Utils.LoadGameObject(typeof(T).Name, Utils.GetParent<T>().ToString());
        }

        public Dictionary<Const.Items, int> GetInventory()
        {
            return inventory;
        }

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
        }

        /*public void AddItem(Const.Items item, int count)
        {
            if (!inventory.ContainsKey(item))
            {
                inventory.Add(item, count);
                return;
            }

            inventory[item] += count;
            /*var maxSpace = GetMaxSpaceForItem(item);

            if (maxSpace > 0)
            {
                if (maxSpace - count < 0)
                {
                    var itemsToAdd = count - Math.Abs(maxSpace - count);
                    inventory.Add(item, itemsToAdd);
                    return itemsToAdd;
                }

                inventory.Add(item, count);
            }

            return 0;
        }

        public void RemoveItem(Const.Items item, int count)
        {
            inventory[item] -= count;
        }

        public void GetFrom(GameObject gm, Const.Items item, int count)
        {
            var maxSpace = GetMaxSpaceForItem(item);


            //je na item místo
            if (maxSpace > 0)
            {
                //na item není po změně místo
                if (maxSpace - count < 0)
                {
                    var itemsToAdd = count - Math.Abs(maxSpace - count);
                    inventory.Add(item, itemsToAdd);

                    gm.GetComponent<CustomObject>().RemoveItem(item, count);
                    AddItem(item, count);
                }

                inventory.Add(item, count);
            }
        }

        public Dictionary<Const.Items, int> GetInventory()
        {
            return inventory;
        }

        private int GetMaxSpaceForItem(Const.Items item)
        {
            var maxCap = 99 * itemSlotLimit;

            foreach (var i in inventory)
                if (i.Key != item)
                    maxCap -= decimal.ToInt32(Math.Ceiling((decimal) (i.Value / 99))) * 99 - i.Value;

            return maxCap;
        }
    }*/

        /*            var slots = 0;
                var listSloty = new Dictionary<Const.Items, int>();
    
                foreach (var i in inventory)
                {
                    var mezi = int.Parse(Math.Ceiling((decimal) (i.Value / 99)).ToString(CultureInfo.InvariantCulture));
                    slots += mezi;
                    listSloty.Add(item, mezi);
                }*/
    }
}