using Enums;

namespace Models.WorldObjects.Buildings;

public class House : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.House;
}
