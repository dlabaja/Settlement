using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using Tree = Assets.Scripts.Buildings.Tree;

namespace Assets.Scripts
{
    public class Woodcutter : Building, ICollideable
    {
        public async void OnCollision(Entity entity)
        {
            await entity.Stop(2000);
            entity.SetDestination(entity.FindNearestObject<Tree>());
        }
    }
}