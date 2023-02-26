using Interfaces;
using System;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Spawn : Workplace, ICollideable
    {
        public Task OnCollision(Entity entity) => Task.CompletedTask;
    }
}