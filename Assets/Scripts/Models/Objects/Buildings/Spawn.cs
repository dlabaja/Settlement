using Enums;

namespace Models.Objects.Buildings;

public class Spawn : CustomObject
{
    public override CustomObjectType CustomObjectType { get; } = CustomObjectType.Spawn;
}
