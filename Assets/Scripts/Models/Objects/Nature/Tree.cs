using Enums;

namespace Models.Objects.Nature;

public class Tree : CustomObject
{
    public override CustomObjectType CustomObjectType { get; } = CustomObjectType.Tree;
}
