using Assets.Scripts.Buildings;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class Woodcutter : Building, ICollideable, IInventoryPickable
    {
        public async void OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            entity.FindObject<Tree>();
        }
    }
}