using Enums;
using Models.WorldObjects;
using Models.WorldObjects.Buildings;
using Models.WorldObjects.Nature;

namespace Factories;

public class WorldObjectFactory
{
    public WorldObject Create(WorldObjectType type)
    {
        return type switch
        {
            WorldObjectType.Spawn => new Spawn(),
            WorldObjectType.Tree => new Tree(),
            WorldObjectType.BerryBush => new BerryBush(),
            WorldObjectType.House => new House(),
            WorldObjectType.Church => new Church(),
            WorldObjectType.Well => new Well()
        };
    }
}
