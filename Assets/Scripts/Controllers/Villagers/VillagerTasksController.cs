using Models.Villagers;
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
    private readonly GameObject _villagerGameObject;
    public bool WaitingForTasks { get; private set; } = true;

    public VillagerTasksController(Villager villager, VillagerMovement villagerMovement, GameObject villagerGameObject, WorldObjectsService worldObjectsService)
    {
        _villager = villager;
        _villagerMovement = villagerMovement;
        _worldObjectsService = worldObjectsService;
        _villagerGameObject = villagerGameObject;
    }

    public void ProcessTask()
    {
        if (!WaitingForTasks)
        {
            return;
        }

        var tasks = _villager.Tasks;
        var hasTasks = _villager.Tasks.TryPeek(out var task);
        if (!hasTasks)
        {
            // work
            WaitingForTasks = false;
            return;
        }

        var taskHasDestination = _worldObjectsService.TryGetNearestEntryPoint(task.Destination, _villagerGameObject.transform.position, out var point);
        if (!taskHasDestination)
        {
            if (tasks.Length == 1)
            {
                // přidej task na práci
                return;
            }
            tasks.PromoteNextTask();
            return;
        }

        _villagerMovement.SetDestination(point.Position);
        // checkni jestli tam může dojít
        // nějaká kontrola jak dodělat task
    }
}
