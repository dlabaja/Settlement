using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Buildings
{
    public class Tree : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            gameObject.GetComponent<Inventory>().TransferItems(entity.gameObject, Const.Item.Wood, 3);
            Destroy(gameObject);
            entity.FindObject<Tree>();
        }
    }
}