using Enums;
using Models.Systems.Inventory;

namespace Models.WorldObjects.Nature;

public class BerryBush : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.BerryBush;
    public override Inventory Inventory { get; } = new Inventory(1);
}
