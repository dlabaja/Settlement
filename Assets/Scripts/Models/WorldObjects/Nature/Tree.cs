using Enums;
using Models.Systems.Inventory;

namespace Models.WorldObjects.Nature;

public class Tree : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Tree;
    public override Inventory Inventory { get; } = new Inventory(1);
}
