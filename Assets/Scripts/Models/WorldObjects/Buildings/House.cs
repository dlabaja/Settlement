using Enums;
using Models.Systems.Inventory;

namespace Models.WorldObjects.Buildings;

public class House : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.House;
    public override Inventory Inventory { get; } = new Inventory(0);
}
