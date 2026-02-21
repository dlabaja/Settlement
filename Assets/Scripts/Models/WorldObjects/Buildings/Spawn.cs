using Enums;
using Models.Systems.Inventory;

namespace Models.WorldObjects.Buildings;

public class Spawn : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Spawn;
    public override Inventory Inventory { get; } = new Inventory(0);
}
