using Models.WorldObjects;
using System.Threading.Tasks;

namespace Models.Villagers.Tasks;

public delegate Task VillagerTaskDelegate(Villager villager);

public class VillagerTask
{
    public Villager Source { get; }
    public WorldObject Destination { get; }
    public VillagerTaskDelegate Task { get; }
    public double WaitTime { get; } 

    public VillagerTask(Villager source, WorldObject destination, VillagerTaskDelegate fn, double waitTime)
    {
        Source = source;
        Destination = destination;
        Task = fn;
        WaitTime = waitTime;
    }
}
