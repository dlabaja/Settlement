using Enums;
using Models.WorldObjects;
using Models.WorldObjects.Buildings;
using Models.WorldObjects.Nature;
using Services;

namespace Factories;

public class WorldObjectFactory
{
    private readonly TimerService _timerService;

    public WorldObjectFactory(TimerService timerService)
    {
        _timerService = timerService;
    }
    
    public WorldObject Create(WorldObjectType type)
    {
        return type switch
        {
            WorldObjectType.Spawn => new Spawn(),
            WorldObjectType.Tree => new Tree(),
            WorldObjectType.BerryBush => new BerryBush(_timerService),
            WorldObjectType.House => new House(),
            WorldObjectType.Church => new Church(),
            WorldObjectType.Well => new Well()
        };
    }
}
