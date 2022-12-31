using Interfaces;
using System.Threading.Tasks;

namespace Buildings
{
    public class Well : Building, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            entity.RefillWater();
            await entity.Stop(2000);
            //todo a) po stromech bude kvůli reforestaci zůstavat empty object
            //todo b) budou se spawnovat náhodně vedle sebe
        }
    }
}