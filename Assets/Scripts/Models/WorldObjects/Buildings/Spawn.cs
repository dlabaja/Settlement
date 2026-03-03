using Enums;
using Models.Systems.Inventory;
using UnityEngine;
using System.Threading.Tasks;

namespace Models.WorldObjects.Buildings;

public class Spawn : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Spawn;
    public override Inventory Inventory { get; } = new Inventory(0);
    public override Task VillagerTask(Villagers.Villager source, WorldObject destination) => throw new System.NotImplementedException();
}
