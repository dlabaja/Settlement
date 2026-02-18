using Enums;

namespace Models.WorldObjects.Nature;

public class Tree : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Tree;
}
