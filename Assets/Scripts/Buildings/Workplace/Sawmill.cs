using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Sawmill : Workplace, ICollideable
    {
        public void Awake()
        {
            WorkObject = Const.Buildings.Sawmill;
            MaxWorkers = 2;
            ProducingItems = (new List<Const.Item>{Const.Item.Wood}, new List<Const.Item>{Const.Item.Planks});
        }
        
        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }
    }
}
