using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Forester : Workplace, ICollideable
    {
        public void Awake()
        {
            WorkObject = Const.Buildings.Tree;
            MaxWorkers = 1;
            ProducingItems = (new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.None});
        }
        
        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }
    }
}
