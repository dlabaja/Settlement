using Enums;
using Models.Systems.Inventory;
using Models.Villagers;
using UnityEngine;
using System.Threading.Tasks;

namespace Models.WorldObjects.Buildings;

public class Spawn : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Spawn;
    public override Inventory Inventory { get; } = new Inventory(0);
    
    public override Task VillagerTask(Villager villager)
    {
        Debug.Log("Pracuju na spawnu");
        return Task.CompletedTask;
    }
}
