using Delegates;
using Models.WorldObjects;

namespace Models.Villagers.Tasks;


public class VillagerTask
{
    public Villager Source { get; }
    public WorldObject Destination { get; }
    public VillagerTaskDelegate Task { get; }

    public VillagerTask(Villager source, WorldObject destination, VillagerTaskDelegate fn)
    {
        Source = source;
        Destination = destination;
        Task = fn;
    }
}
