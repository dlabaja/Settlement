using Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings
{
    public class House : Building, ICollideable
    {
        [SerializeField] private int capacity;
        [SerializeField] private List<Entity> inhabitants = new();

        public bool HasFreeRoom() => capacity - inhabitants.Count != 0;
        
        public async Task OnCollision(Entity entity)
        {
            //await entity.Stop(1000);
            await Task.Delay(2000);
            entity.RefillSleep();
        }
    }
}