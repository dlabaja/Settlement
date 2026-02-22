using Enums;
using Models.Systems.Inventory;
using System.Threading.Tasks;
using UnityEngine;

namespace Models.WorldObjects;

public abstract class WorldObject
{
    public abstract WorldObjectType WorldObjectType { get; }
    public abstract Inventory Inventory { get; }
    public abstract Task VillagerTask(Villagers.Villager source, Vector3 destination);
}
