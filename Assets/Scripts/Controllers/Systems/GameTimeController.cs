using Services;
using System;

namespace Controllers.Systems;

public class GameTimeController
{
    private readonly GameTimeService _gameTimeService;
    private int counter;

    public GameTimeController(GameTimeService gameTimeService)
    {
        _gameTimeService = gameTimeService;
    }
    
    public bool TryTick()
    {
        if (_gameTimeService.IsPaused)
        {
            return false;
        }

        counter++;
        if (counter % SpeedToMod(_gameTimeService.GameSpeed) != 0)
        {
            return false;
        }
        
        _gameTimeService.Tick();
        return true;
    }

    private int SpeedToMod(int speed)
    {
        return Math.Abs(speed - _gameTimeService.MaxSpeed) + 1;
    }
}
