using Interfaces;
using System;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Spawn : Workplace, ICollideable, IStats
    {
        public Task OnCollision(Entity entity) => Task.CompletedTask;

        public void GenerateStats()
        {
        }
    }
}
