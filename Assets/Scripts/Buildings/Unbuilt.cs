using Gui.Stats;
using Interfaces;
using Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Buildings
{
    public class Unbuilt : Building, IStats
    {
        private List<ItemStruct> requiredItems = new List<ItemStruct>();
        private List<ItemStruct> deliveredItems = new List<ItemStruct>();

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = GameObject.Find("Spawn").GetComponent<Mesh>(); //todo vlastní model
        }

        public void GenerateStats()
        {
            Stats.GenerateStats(gameObject)
                .AddLabel(name, 20)
                .AddLabel(DeliveredMaterials)
                .BuildWindow();
        }

        private string DeliveredMaterials()
        {
            var str = new List<string>();
            for (int i = 0; i < requiredItems.Count; i++)
                str.Add($"{deliveredItems[i]}/{requiredItems[i]}");
            return string.Join(",", str);
        }

        public List<ItemStruct> GetNeededItems()
        {
            var ls = new List<ItemStruct>();
            
        }
    }
}
