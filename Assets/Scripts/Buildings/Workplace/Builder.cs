using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Builder : Workplace, ICollideable
    {
        public void Awake()
        {
            MaxWorkers = 3;
            ProducingItems = (new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.None});
        }
        
        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }

        public void CutAllTrees(List<GameObject> trees)
        {
            
        }
    }
}
