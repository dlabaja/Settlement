using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICollideable
    {
        public Task OnCollision(Entity entity);
    }
}