using DataTypes;
using Enums;
using JetBrains.Annotations;
using Models.WorldObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Villagers.VillagerTasks;

public class VillagerTasks
{
    private readonly PriorityQueue<VillagerTask> _tasks = new PriorityQueue<VillagerTask>();
    private List<WorldObject> _addedDestinations = new List<WorldObject>();
    public bool IsEmpty => _tasks.IsEmpty;
    public int Length => _tasks.Length;
    [CanBeNull] public VillagerTask CurrentRunningTask { get; private set; }
    public event Action TaskStarted; 
    public event Action TaskCompleted;
    
    /// <summary>
    ///     Adds a task with priority if it's not already added
    /// </summary>
    /// <param name="villagerTask"></param>
    /// <param name="priority"></param>
    public void Add(VillagerTask villagerTask, TaskPriority priority)
    {
        if (_addedDestinations.Contains(villagerTask.Destination))
        {
            return;
        }
        _tasks.Add(villagerTask, (int)priority);
        _addedDestinations.Add(villagerTask.Destination);
    }

    public void Remove(VillagerTask villagerTask)
    {
        _tasks.Remove(villagerTask);
        _addedDestinations.Remove(villagerTask.Destination);
    }

    public bool TryPeek(out VillagerTask task)
    {
        task = null;
        return _tasks.TryPeek(out task);
    }
    
    public bool TryPop(out VillagerTask task)
    {
        task = null;
        var popped = _tasks.TryPop(out task);
        if (!popped)
        {
            return false;
        }
        
        _addedDestinations.Remove(task.Destination);
        return true;
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
