using Assets.Scripts.Interfaces;
using System.Threading.Tasks;

namespace Assets.Scripts.Buildings.Workplace
{
    public class Woodcutter : Workplace, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            //entity.SetDestination(entity.FindNearestObject<Tree>());
        }
    }
}