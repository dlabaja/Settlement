using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Buildings
{
    public class House : Building, ICollideable
    {
        [SerializeField] private int capacity;
        [SerializeField] private List<Entity> inhabitants = new();

        private void Start()
        {
        }

        public bool HasFreeRoom() => capacity - inhabitants.Count != 0;
        
        public async Task OnCollision(Entity entity)
        {
            //await entity.Stop(1000);
            await Task.Delay(2000);
            entity.RefillSleep();
        }
    }
}