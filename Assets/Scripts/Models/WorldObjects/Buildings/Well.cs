using Enums;
using Models.Systems.Inventory;
using UnityEngine;
using System.Threading.Tasks;

namespace Models.WorldObjects.Buildings;

public class Well : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Well;
    public override Inventory Inventory { get; } = new Inventory(0);
    public override Task VillagerTask(Villagers.Villager source, WorldObject destination)
    {
        Debug.Log("jdu ke studni");
        return Task.CompletedTask;
    }
}
