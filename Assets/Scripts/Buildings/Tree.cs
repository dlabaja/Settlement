using Interfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings
{
    public class Tree : Building, ICollideable, IIgnoreGlobalInventory
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            var inventory = GetComponent<Inventory.Inventory>();
            inventory.TransferItems(Const.Item.Wood, 3, entity.gameObject);
            if (inventory.IsEmpty())
                gameObject.SetActive(false);
        }
    }
}