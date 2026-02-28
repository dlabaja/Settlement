using Services;
using UnityEngine;
using UnityEngine.AI;

namespace Models.Villagers;

public class VillagerMovement
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly GameTimeService _gameTimeService;
    private const int _speed = 10;

    public VillagerMovement(NavMeshAgent villagerNavMeshAgentAgent, GameTimeService gameTimeService)
    {
        _navMeshAgent = villagerNavMeshAgentAgent;
        _gameTimeService = gameTimeService;
        _gameTimeService.SpeedChanged += GameTimeServiceOnSpeedChanged;
    }

    public bool CanReach(Vector3 destination, out NavMeshPath path)
    {
        path = new NavMeshPath();
        return _navMeshAgent.CalculatePath(destination, path);
    }

    public void SetDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
    }
    
    public void SetPath(NavMeshPath navMeshPath)
    {
        _navMeshAgent.SetPath(navMeshPath);
    }

    public void Walk()
    {
        _navMeshAgent.isStopped = false;
    }

    public void Stop()
    {
        _navMeshAgent.isStopped = true;
    }
    
    private void GameTimeServiceOnSpeedChanged()
    {
        _navMeshAgent.speed = _speed * _gameTimeService.GameSpeed;
    }
}
