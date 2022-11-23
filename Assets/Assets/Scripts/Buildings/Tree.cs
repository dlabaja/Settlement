using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Buildings
{
    public class Tree : Building, ICollideable, IGlobalInventoryBlacklist
    {
        public void OnCollision(Entity entity)
        {
            //await entity.Stop(2000);
            gameObject.GetComponent<Inventory.Inventory>().TransferItems(entity.gameObject, Const.Item.Wood, 3);
            Destroy(gameObject);
        }
    }
}