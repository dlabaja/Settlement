using Enums;

namespace Models.WorldObjects.Buildings;

public class Spawn : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Spawn;
}
