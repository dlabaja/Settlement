using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Buildings
{
    public class Tree : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            Destroy(gameObject);
            entity.AddItem(Const.Items.Wood, 2);
            entity.FindObject<Tree>();
        }
    }
}