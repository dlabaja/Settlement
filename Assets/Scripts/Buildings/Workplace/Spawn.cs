using Interfaces;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Spawn : Workplace, ICollideable, IStats
    {
        public Task OnCollision(Entity entity) => Task.CompletedTask;

        public new void GenerateStats()
        {
        }
    }
}
