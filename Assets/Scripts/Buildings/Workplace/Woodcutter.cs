using Interfaces;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Woodcutter : Workplace, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            await Task.Delay(2000);
            //entity.SetDestination(entity.FindNearestObject<Tree>());
        }
    }
}