using Interfaces;
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
                await entity.Stop(2000);
                entity.GetComponent<Inventory.Inventory>().AddItems(Const.Item.Stone, 1);   
            }
            //entity.ChangeLookingFor();
        }
    }
}
