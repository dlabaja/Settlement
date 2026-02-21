using Services;

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
        if (_gameTimeService.IsPaused || counter % _gameTimeService.GameSpeed != 0)
        {
            return false;
        }
        _gameTimeService.Tick();
        counter++;
        
        return true;
    }
}
