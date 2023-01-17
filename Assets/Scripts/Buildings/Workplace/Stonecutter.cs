using Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Stonecutter : Workplace, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            GetComponent<Inventory.Inventory>().TransferItems(Const.Item.Stone,
                entity.GetComponent<Inventory.Inventory>().GetItemCount(Const.Item.Stone), gameObject, entity.gameObject);
        }
    }
}
