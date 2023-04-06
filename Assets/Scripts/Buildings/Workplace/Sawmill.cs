using Interfaces;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Sawmill : Workplace, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }
    }
}
