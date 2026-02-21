using Enums;
using Models.Systems.Inventory;

namespace Models.WorldObjects.Buildings;

public class Church : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Church;
    public override Inventory Inventory { get; } = new Inventory(0);
}
