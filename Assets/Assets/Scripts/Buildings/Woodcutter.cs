using Assets.Scripts.Buildings;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class Woodcutter : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            entity.FindObject<Tree>();
        }
    }
}