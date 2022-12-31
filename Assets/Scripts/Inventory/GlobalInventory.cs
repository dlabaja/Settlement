using Gui;
using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class GlobalInventory : MonoBehaviour
    {
        private static Dictionary<Const.Item, int> _globalInventory = new();

        private void Start()
        {
            InvokeRepeating(nameof(OnceASecUpdate), 1, 1);
        }

        private void OnceASecUpdate()
        {
            UpdateGlobalInventory();
            Hud.SetGlobalInventoryText(Utils.DictToString(_globalInventory));
        }

        private static void UpdateGlobalInventory()
        {
            var customObjects = FindObjectsOfType<CustomObject>();
            foreach (var item in customObjects) //all object inventories
            {
                var gm = item.gameObject;
                if (gm.HasComponent<IIgnoreGlobalInventory>() || !gm.HasComponent<Inventory>()) return; //has inventory and isn't blacklisted

                foreach (var i in gm.GetComponent<Inventory>().GetInventory()) //all items in object inventory
                {
                    UpsertGlobalInventory(i.Key, i.Value);
                }

            }
        }

        //adds items to stack or creates new slot for them
        private static void UpsertGlobalInventory(Const.Item item, int count)
        {
            if (!_globalInventory.TryAdd(item, count)) _globalInventory[item] = count;
        }
    }
}
