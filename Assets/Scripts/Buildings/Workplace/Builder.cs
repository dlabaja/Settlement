using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Builder : Workplace, ICollideable
    {
        private void Awake()
        {
            
        }

        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }
    }
}
