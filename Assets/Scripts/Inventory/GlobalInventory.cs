using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory
{
    public class GlobalInventory : MonoBehaviour
    {
        private static readonly Dictionary<Const.Item, int> _globalInventory = new();

        private void Awake()
        {
            foreach (var i in Enum.GetValues(typeof(Const.Item)).Cast<Const.Item>())
                if (i != Const.Item.None)
                    _globalInventory.TryAdd(i, 0);
            try
            {
                var gI = GetComponent<UIDocument>().rootVisualElement;
                StartCoroutine(UpdateGlobalInventory(gI));
            }
            catch {}
        }

        private static IEnumerator UpdateGlobalInventory(VisualElement root)
        {
            while (true)
            {
                UpdateGlobalInventoryValues();
                root.Q<Label>("Text").text = Utils.DictToString(_globalInventory);
                yield return new WaitForSeconds(1);
            }
        }

        private static void UpdateGlobalInventoryValues()
        {
            _globalInventory.Clear();

            foreach (var item in FindObjectsOfType<Inventory>())
            {
                if (item.gameObject.HasComponent<IIgnoreGlobalInventory>()) continue;

                foreach (var i in item.GetInventory().Values)
                {
                    if (i.item == Const.Item.None) continue;
                    if (!_globalInventory.ContainsKey(i.item)) _globalInventory[i.item] = 0;
                    _globalInventory[i.item] += i.count;
                }
            }
        }
    }
}
