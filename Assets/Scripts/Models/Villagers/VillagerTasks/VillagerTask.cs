using Delegates;
using UnityEngine;

namespace Models.Villagers.VillagerTasks;

public class VillagerTask
{
    public Vector3 Destination { get; }
    public Villagers.Villager Source { get; }
    public TaskDelegate Task { get; }

    public VillagerTask(Villagers.Villager source, Vector3 destination, TaskDelegate fn)
    {
        Source = source;
        Destination = destination;
        Task = fn;
    }
}
