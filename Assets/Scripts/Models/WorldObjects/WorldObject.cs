using Enums;
using Models.Systems.Inventory;

namespace Models.WorldObjects;

public abstract class WorldObject
{
    public abstract WorldObjectType WorldObjectType { get; }
    public abstract Inventory Inventory { get; }
}
