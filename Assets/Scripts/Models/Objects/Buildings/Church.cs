using Enums;

namespace Models.Objects.Buildings;

public class Church : CustomObject
{
    public override CustomObjectType CustomObjectType { get; } = CustomObjectType.Church;
}
