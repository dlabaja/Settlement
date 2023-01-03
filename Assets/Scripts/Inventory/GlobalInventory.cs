using Buildings.Workplace;
using Gui;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class GlobalInventory : MonoBehaviour
    {
        private static Dictionary<Const.Item, int> _globalInventory = new();

        private void Start()
        {
            foreach (var i in Enum.GetValues(typeof(Const.Item)).Cast<Const.Item>())
                if (i != Const.Item.None)
                    _globalInventory.TryAdd(i, 0);
            StartCoroutine(UpdateGlobalInventory());
        }

        private static IEnumerator UpdateGlobalInventory()
        {
            while (true)
            {
                UpdateGlobalInventoryValues();
                Hud.SetGlobalInventoryText(Utils.DictToString(_globalInventory));
                yield return new WaitForSeconds(1);
            }
        }

        private static void UpdateGlobalInventoryValues()
        {
            //todo není tam dřevorubec
            foreach (var key in _globalInventory.Keys.ToList()) { _globalInventory[key] = 0; }
            foreach (var item in FindObjectsOfType<Inventory>()) //all object inventories
            {
                if (item.gameObject.HasComponent<IIgnoreGlobalInventory>()) return; //has inventory and isn't blacklisted

                foreach (var i in item.GetInventory().Values) //all items in object inventory
                {
                    if (i.item == Const.Item.None) continue;
                    _globalInventory[i.item] += i.count;
                }

            }
        }
    }
}
