using Enums;
using Models.Villagers;
using Models.Villagers.Tasks;
using Models.WorldObjects;

namespace Factories;

public class VillagerTaskFactory
{
    public VillagerTask Create(Villager villager, WorldObject worldObject)
    {
        return new VillagerTask(villager, worldObject, worldObject.VillagerTask,
            VillagerTasks.GetTaskWaitTime(worldObject.WorldObjectType));
    }

    public VillagerTask WorkTask(Villager villager)
    {
        return new VillagerTask(villager, villager.Places.Workplace, villager.Places.Workplace.VillagerTask, 
            VillagerTasks.GetTaskWaitTime(villager.Places.Workplace.WorldObjectType));
    }

    public VillagerTask DrinkTask(Villager villager, WorldObject nearestWell)
    {
        return new VillagerTask(villager, nearestWell, nearestWell.VillagerTask,
            VillagerTasks.GetTaskWaitTime(WorldObjectType.Well));
    }
}
