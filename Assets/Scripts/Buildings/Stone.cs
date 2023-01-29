using Buildings.Workplace;
using Interfaces;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings
{
    public class Stone : Building, ICollideable, IIgnoreGlobalInventory
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            while (!entity.GetComponent<Inventory.Inventory>().IsFull())
            {
                //todo během práce leavne job
                await entity.Stop(2000);
                entity.GetComponent<Inventory.Inventory>().AddItems(Const.Item.Stone, 1);
            }
        }
    }
}
