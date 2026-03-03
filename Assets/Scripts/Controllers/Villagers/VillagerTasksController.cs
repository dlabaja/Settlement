using Enums;
using Factories;
using Models.Villagers;
using Models.Villagers.VillagerTasks;
using Services;
using Services.GameObjects;
using UnityEngine;

namespace Controllers.Villagers;

public class VillagerTasksController
{
    private readonly GameTimeService _gameTimeService;
    private readonly WorldObjectsService _worldObjectsService;
    private readonly Villager _villager;
    private readonly VillagerMovement _villagerMovement;
    private readonly VillagerTasks _tasks;
    private readonly VillagerTaskFactory _villagerTaskFactory;
    private readonly PathfindingService _pathfindingService;

    public VillagerTasksController(Villager villager, VillagerMovement villagerMovement, 
        WorldObjectsService worldObjectsService, PathfindingService pathfindingService, VillagerTaskFactory villagerTaskFactory)
    {
        _villager = villager;
        _villagerMovement = villagerMovement;
        _worldObjectsService = worldObjectsService;
        _pathfindingService = pathfindingService;
        _villagerTaskFactory = villagerTaskFactory;
        _tasks = _villager.Tasks;
    }

    public void ProcessTask(Vector3 villagerPos)
    {
        if (_tasks.CurrentRunningTask != null)
        {
            return;
        }

        var hasTasks = _tasks.TryPeek(out var task);
        if (!hasTasks)
        {
            AddWorkTask();
            return;
        }

        var taskHasDestination = _worldObjectsService.TryGetNearestEntryPoint(task.Destination, villagerPos, out var point);
        if (!taskHasDestination)
        {
            if (_tasks.Length == 1)
            {
                AddWorkTask();
                return;
            }
            _tasks.PromoteNextTask();
            return;
        }

        DoTask(villagerPos, point.Position);
    }

    private void DoTask(Vector3 villagerPos, Vector3 destination)
    {
        var canReach = _pathfindingService.CanReach(villagerPos, destination, out var path);
        _villagerMovement.SetDestination(destination, path);
        // checkni jestli tam může dojít
        // nějaká kontrola jak dodělat task
    }

    private void AddWorkTask()
    {
        _tasks.Add(_villagerTaskFactory.WorkTask(_villager), TaskPriority.Low);
    }
}
