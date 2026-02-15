using Enums;
using Models.Objects;
using Models.Objects.Buildings;

namespace Factories;

public class CustomObjectFactory
{
    public CustomObject Create(CustomObjectType type)
    {
        return type switch
        {
            CustomObjectType.Spawn => new Spawn(),
            CustomObjectType.Tree => new Spawn(),
            CustomObjectType.BerryBush => new Spawn(),
            CustomObjectType.House => new Spawn(),
            CustomObjectType.Church => new Spawn(),
        };
    }
}
