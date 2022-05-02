using Assets.Scripts.Buildings;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class Woodcutter : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            await entity.Stop(2000);

            if (FindObjectsOfType<Tree>().Length != 0) entity.FindObject<Tree>();
        }
    }
}