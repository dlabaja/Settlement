using Delegates;
using Models.WorldObjects;

namespace Models.Villagers.VillagerTasks;

public class VillagerTask
{
    public WorldObject Destination { get; }
    public Villager Source { get; }
    public TaskDelegate Task { get; }

    public VillagerTask(Villager source, WorldObject destination, TaskDelegate fn)
    {
        Source = source;
        Destination = destination;
        Task = fn;
    }
}
