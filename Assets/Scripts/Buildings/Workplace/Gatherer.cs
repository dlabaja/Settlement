using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Gatherer : Workplace, ICollideable
    {
        private void Awake()
        {
            producingItems = (new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.Berries});
        }

        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            GetComponent<Inventory.Inventory>().TransferItems(Const.Item.Berries,
                entity.GetComponent<Inventory.Inventory>().GetItemCount(Const.Item.Berries),
                gameObject,
                entity.gameObject);
        }
    }
}
