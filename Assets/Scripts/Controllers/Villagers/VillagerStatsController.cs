using Models.Villagers;
using Services;
using Utils;

namespace Controllers.Villagers;

public class VillagerStatsController
{
    private readonly Villager _villager;
    private readonly GameTimeService _gameTimeService;
    
    public VillagerStatsController(Villager villager, GameTimeService gameTimeService)
    {
        _villager = villager;
        _gameTimeService = gameTimeService;
        _gameTimeService.TimeTicked += DecreaseWater;
        _gameTimeService.TimeTicked += DecreaseFood;
        _gameTimeService.TimeTicked += DecreaseChurch;
        _gameTimeService.TimeTicked += DecreaseHousing;
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
        RandomUtils.CheckChance(20, _villager.Stats.Water.Decrease);
    }
    
    private void DecreaseFood()
    {
        RandomUtils.CheckChance(50, _villager.Stats.Food.Decrease);
    }
    
    private void DecreaseChurch()
    {
        RandomUtils.CheckChance(80, _villager.Stats.Church.Decrease);
    }
    
    private void DecreaseHousing()
    {
        RandomUtils.CheckChance(60, _villager.Stats.Housing.Decrease);
    }
}
