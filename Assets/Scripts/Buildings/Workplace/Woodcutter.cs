using Gui.Stats;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Woodcutter : Workplace, ICollideable, IStats
    {
        private void Awake()
        {
            producingItems = new Dictionary<List<Const.Item>, List<Const.Item>>{
                {new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.Wood}} 
            };
        }

        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            GetComponent<Inventory.Inventory>().TransferItems(Const.Item.Wood,
                entity.GetComponent<Inventory.Inventory>().GetItemCount(Const.Item.Wood), gameObject, entity.gameObject);
        }


        public void GenerateStats()
        {
            Stats.GenerateStats()
                .AddLabel("Woodcutter", 20)
                .AddLabel("Producing Wood")
                .AddSpace()
                .AddLabelWithText("Workers", "6")
                .AddLabelWithText("Efficiency", "100%")
                .AddSpace()
                .AddLabel("Woodcutter")
                .AddLabelWithText("Workers", "6")
                .AddLabel("Woodcutter")
                .AddLabel("-> Wood")
                .AddLabelWithText("Workers", "6")
                .AddSpace()
                .AddSpace()
                .AddAssignDropdown(gameObject)
                .AddLabelWithText("Workers", "6")
                .BuildStats();
        }
    }
}