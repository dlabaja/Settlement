using Delegates;
using UnityEngine;

namespace Models.Villager.VillagerTasks;

public class VillagerTask
{
    public Vector3 Destination { get; }
    public Villager Source { get; }
    public TaskDelegate Task { get; }

    public VillagerTask(Villager source, Vector3 destination, TaskDelegate fn)
    {
        Source = source;
        Destination = destination;
        Task = fn;
    }
}
