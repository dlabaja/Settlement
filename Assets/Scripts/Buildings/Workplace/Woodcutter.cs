using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Woodcutter : Workplace, ICollideable
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
    }
}