using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class Well : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            entity.RefillWater();
        }
    }
}