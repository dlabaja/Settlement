using Assets.Scripts.Interfaces;
using System.Threading.Tasks;

namespace Assets.Scripts.Buildings
{
    public class Tree : Building, ICollideable, IGlobalInventoryBlacklist
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            gameObject.GetComponent<Inventory.Inventory>().TransferItems(entity.gameObject, Const.Item.Wood, 3);
            Destroy(gameObject);
        }
    }
}