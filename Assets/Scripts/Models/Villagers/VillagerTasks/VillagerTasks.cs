using DataTypes;
using Enums;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;

namespace Models.Villagers.VillagerTasks;

public class VillagerTasks
{
    private readonly PriorityQueue<VillagerTask> _tasks = new PriorityQueue<VillagerTask>();
    public bool IsEmpty => _tasks.IsEmpty;
    public int Length => _tasks.Length;
    [CanBeNull] public VillagerTask CurrentRunningTask { get; private set; }
    public event Action TaskStarted; 
    public event Action TaskCompleted;

    public void Add(VillagerTask villagerTask, TaskPriority priority)
    {
        _tasks.Add(villagerTask, (int)priority);
    }

    public void Remove(VillagerTask villagerTask)
    {
        _tasks.Remove(villagerTask);
    }

    public bool TryPeek(out VillagerTask task)
    {
        task = null;
        return _tasks.TryPeek(out task);
    }
    
    public bool TryPop(out VillagerTask task)
    {
        task = null;
        return _tasks.TryPop(out task);
    }

    public void PromoteNextTask()
    {
        if (_tasks.Length < 2)
        {
            return;
        }
        _tasks.Promote(_tasks.Items[1], _tasks.HighestPriority + 1);
    }

    public void SetTask()
    {
        if (!_tasks.TryPop(out var task))
        {
            return;
        }
        CurrentRunningTask = task;
    }

    public async Task RunCurrentTask()
    {
        if (CurrentRunningTask == null)
        {
            return;
        }
        
        TaskStarted?.Invoke();
        await CurrentRunningTask.Task(CurrentRunningTask.Source, CurrentRunningTask.Destination);
        CurrentRunningTask = null;
        TaskCompleted?.Invoke();
    }
}
