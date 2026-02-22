using Enums;
using Models.Systems.Inventory;
using System.Threading.Tasks;
using UnityEngine;

namespace Models.WorldObjects.Nature;

public class Tree : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Tree;
    public override Inventory Inventory { get; } = new Inventory(1);
    public override Task VillagerTask(Villagers.Villager source, Vector3 destination) => throw new System.NotImplementedException();
}
