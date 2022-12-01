using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface ICollideable
    {
        public Task OnCollision(Entity entity);
    }
}