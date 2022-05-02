using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomObject : MonoBehaviour
    {
        public int itemSlotLimit = 1;
        private readonly Dictionary<Const.Items, int> inventory = new();

        private void OnDestroy()
        {
            foreach (var entity in FindObjectsOfType<Entity>()) entity.RemoveFromLookingFor(gameObject);
        }

        public static void Spawn<T>()
        {
            print(typeof(T).Name);
            Utils.LoadGameObject(typeof(T).Name, Utils.GetParent<T>().ToString());
        }

        public void AddItem(Const.Items item, int count)
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

            return 0;*/
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
    }

    /*            var slots = 0;
            var listSloty = new Dictionary<Const.Items, int>();

            foreach (var i in inventory)
            {
                var mezi = int.Parse(Math.Ceiling((decimal) (i.Value / 99)).ToString(CultureInfo.InvariantCulture));
                slots += mezi;
                listSloty.Add(item, mezi);
            }*/
}