using DataTypes;
using Enums;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;

namespace Models.Systems.TaskManager;

public class VillagerTaskManager
{
    public PriorityQueue<VillagerTask> Tasks { get; } = new PriorityQueue<VillagerTask>();
    [CanBeNull] public VillagerTask CurrentRunningTask { get; private set; } = null;
    public event Action OnTaskCompleted; 

    public void Add(VillagerTask villagerTask, TaskPriority priority)
    {
        Tasks.Add(villagerTask, (int)priority);
    }

    public void Remove(VillagerTask villagerTask)
    {
        Tasks.Remove(villagerTask);
    }

    public async Task RunTask()
    {
        if (!Tasks.TryPop(out var task))
        {
            return;
        }

        CurrentRunningTask = task;
        await task.Task(task.Source, task.Destination);
        CurrentRunningTask = null;
        OnTaskCompleted?.Invoke();
    } 
}
