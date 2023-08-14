using Interfaces;
using System;
using System.Threading.Tasks;

namespace Buildings
{
    public class Tree : Building, ICollideable, IIgnoreGlobalInventory
    {
        private void Awake()
        {
            GetComponent<Inventory.Inventory>().AddItems(Const.Item.Wood, Const.WoodDrop);
        }

        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000/Const.GameSpeed);
            var inventory = GetComponent<Inventory.Inventory>();
            inventory.TransferItems(Const.Item.Wood, Const.WoodDrop, entity.gameObject);
            if (inventory.IsEmpty())
                gameObject.SetActive(false);
        }
    }
}