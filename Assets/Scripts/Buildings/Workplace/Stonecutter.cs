using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Stonecutter : Workplace, ICollideable
    {
        public void Awake()
        {
            WorkObject = Const.Buildings.Stone;
            MaxWorkers = 4;
            ProducingItems = (new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.Stone});
        }

        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            GetComponent<Inventory.Inventory>().TransferItems(Const.Item.Stone,
                entity.GetComponent<Inventory.Inventory>().GetItemCount(Const.Item.Stone),
                gameObject,
                entity.gameObject);
        }
    }

}
