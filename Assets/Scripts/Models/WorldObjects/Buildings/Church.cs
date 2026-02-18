using Enums;

namespace Models.WorldObjects.Buildings;

public class Church : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Church;
}
