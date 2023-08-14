using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildings.Workplace
{
    public class Spawn : Workplace, ICollideable, IStats
    {
        public void Awake()
        {
            WorkObject = Const.Buildings.Spawn;
            MaxWorkers = int.MaxValue;
            ProducingItems = (new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.None});
        }
        
        public Task OnCollision(Entity entity) => Task.CompletedTask;

        public new void GenerateStats()
        {
        }
    }
}
