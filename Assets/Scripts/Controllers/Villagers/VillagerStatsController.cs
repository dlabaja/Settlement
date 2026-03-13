using Enums;
using Factories;
using Models.Villagers;
using Services.GameObjects;
using Services.GameObjects.Villagers;
using Services.Systems;
using System;
using Utils;

namespace Controllers.Villagers;

public class VillagerStatsController : IDisposable
{
    private readonly Villager _villager;
    private readonly GameTimeService _gameTimeService;
    private readonly VillagerTaskFactory _villagerTaskFactory;
    private readonly VillagerService _villagerService;
    private readonly WorldObjectsService _worldObjectsService;
    
    public VillagerStatsController(Villager villager, VillagerService villagerService, WorldObjectsService worldObjectsService,
        GameTimeService gameTimeService, VillagerTaskFactory villagerTaskFactory)
    {
        _villager = villager;
        _gameTimeService = gameTimeService;
        _villagerTaskFactory = villagerTaskFactory;
        _villagerService = villagerService;
        _worldObjectsService = worldObjectsService;
        _gameTimeService.TimeTicked += DecreaseWater;
        _gameTimeService.TimeTicked += DecreaseFood;
        _gameTimeService.TimeTicked += DecreaseChurch;
        _gameTimeService.TimeTicked += DecreaseHousing;
        _villager.Stats.Water.StatLowThresholdReached += WaterOnStatLowThresholdReached;
        _villager.Stats.Church.StatLowThresholdReached += ChurchOnStatLowThresholdReached;
        _villager.Stats.Food.StatLowThresholdReached += FoodOnStatLowThresholdReached;
        _villager.Stats.Housing.StatLowThresholdReached += HousingOnStatLowThresholdReached;
        _villager.Stats.Happiness.StatLowThresholdReached += HappinessOnStatLowThresholdReached;
    }

    private void HappinessOnStatLowThresholdReached()
    {
    }

    private void HousingOnStatLowThresholdReached()
    {
    }

    private void FoodOnStatLowThresholdReached()
    {
    }

    private void WaterOnStatLowThresholdReached()
    {
        var hasPos = _villagerService.TryGetVillagerPosition(_villager, out var position);
        if (!hasPos)
        {
            return;
        }
        
        var hasWell = _worldObjectsService.TryGetNearestObject(WorldObjectType.Well, position, out var well);
        if (!hasWell)
        {
            return;
        }
        
        _villager.Tasks.Add(_villagerTaskFactory.DrinkTask(_villager, well), TaskPriority.Highest);
    }

    private void ChurchOnStatLowThresholdReached()
    {
    }

    public void Dispose()
    {
        _gameTimeService.TimeTicked -= DecreaseWater;
        _gameTimeService.TimeTicked -= DecreaseFood;
        _gameTimeService.TimeTicked -= DecreaseChurch;
        _gameTimeService.TimeTicked -= DecreaseHousing;
    }

    private void DecreaseWater()
    {
        RandomUtils.WhenChance(20, _villager.Stats.Water.Decrease);
    }
    
    private void DecreaseFood()
    {
        RandomUtils.WhenChance(50, _villager.Stats.Food.Decrease);
    }
    
    private void DecreaseChurch()
    {
        RandomUtils.WhenChance(80, _villager.Stats.Church.Decrease);
    }
    
    private void DecreaseHousing()
    {
        RandomUtils.WhenChance(60, _villager.Stats.Housing.Decrease);
    }
}
