using Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public void FindJob()
        {
            foreach (var item in FindObjectsOfType<Unbuilt>())
            {
                if (!availableWorkers.Any()) return;
                var worker = availableWorkers.First();
                
                
                worker.AddDestination(item.gameObject);
                availableWorkers.Remove(worker);
            }
        }
    }
}
