using Enums;
using Models.WorldObjects;
using Models.WorldObjects.Buildings;

namespace Factories;

public class WorldObjectFactory
{
    public WorldObject Create(WorldObjectType type)
    {
        return type switch
        {
            WorldObjectType.Spawn => new Spawn(),
            WorldObjectType.Tree => new Spawn(),
            WorldObjectType.BerryBush => new Spawn(),
            WorldObjectType.House => new Spawn(),
            WorldObjectType.Church => new Spawn(),
        };
    }
}
