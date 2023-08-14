using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Stonemason : Workplace, ICollideable
    {
        public void Awake()
        {
            WorkObject = Const.Buildings.Stonemason;
            MaxWorkers = 2;
            ProducingItems = (new List<Const.Item>{Const.Item.Stone}, new List<Const.Item>{Const.Item.PolishedStone});
        }
        
        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }
    }
}
