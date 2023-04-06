using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Builder : Workplace, ICollideable
    {
        public async Task OnCollision(Entity entity)
        {
            //todo soon
        }

        public void CutAllTrees(List<GameObject> trees)
        {
            
        }
    }
}
