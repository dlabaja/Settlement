using Interfaces;
using System.Threading.Tasks;

namespace Buildings
{
    public class BerryBush : Building, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            while (!entity.GetComponent<Inventory.Inventory>().IsFull())
            {
                await entity.Stop(2000);
                entity.GetComponent<Inventory.Inventory>().AddItems(Const.Item.Berries, 1);
            }
        }
    }
}
