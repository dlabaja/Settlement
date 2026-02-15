using Enums;

namespace Models.Objects.Buildings;

public class House : CustomObject
{
    public override CustomObjectType CustomObjectType { get; } = CustomObjectType.House;
}
