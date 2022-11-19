using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class Well : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            entity.RefillWater();
            await entity.Stop(2000);
        }
    }
}