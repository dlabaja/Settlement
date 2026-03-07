using Models.Villagers;
using Models.Villagers.VillagerTasks;
using Models.WorldObjects;

namespace Factories;

public class VillagerTaskFactory
{
    public VillagerTask Create(Villager villager, WorldObject worldObject)
    {
        return new VillagerTask(villager, worldObject, worldObject.VillagerTask);
    }

    public VillagerTask WorkTask(Villager villager)
    {
        return new VillagerTask(villager, villager.Places.Workplace, villager.Places.Workplace.VillagerTask);
    }

    public VillagerTask DrinkTask(Villager villager, WorldObject nearestWell)
    {
        return new VillagerTask(villager, nearestWell, nearestWell.VillagerTask);
    }
}
