using Interfaces;
using System.Threading.Tasks;

namespace Buildings
{
    public class Tree : Building, ICollideable, IIgnoreGlobalInventory
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            gameObject.GetComponent<Inventory.Inventory>().TransferItems(entity.gameObject, Const.Item.Wood, 3);
            Destroy(gameObject);
        }
    }
}