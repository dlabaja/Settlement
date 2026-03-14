using Models.Villagers;
using Models.WorldObjects;
using Services.Systems;
using System.Threading.Tasks;

namespace Controllers.GameObjects;

public class InteractionPointController
{
    private readonly GameTimeService _gameTimeService;

    public InteractionPointController(GameTimeService gameTimeService)
    {
        _gameTimeService = gameTimeService;
    }
    
    public async Task OnVillagerCollision(Villager villager, WorldObject destination)
    {
        if (villager.Tasks.CurrentRunningTask == null)
        {
            return;
        }

        if (villager.Tasks.CurrentRunningTask.Destination != destination)
        {
            return;
        }

        await _gameTimeService.Wait(villager.Tasks.CurrentRunningTask.WaitTime);
        await villager.Tasks.RunCurrentTask();
    }
}
