using Models.Villagers;
using Models.WorldObjects;
using System.Threading.Tasks;

namespace Controllers.GameObjects;

public class InteractionPointController
{
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
        
        await villager.Tasks.RunCurrentTask();
    }
}
