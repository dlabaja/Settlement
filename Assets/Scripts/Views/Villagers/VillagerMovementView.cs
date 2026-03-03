using Models.Villagers;
using Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Views.Villagers;

public class VillagerMovementView : IDisposable
{
    private readonly VillagerMovement _villagerMovement;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly GameTimeService _gameTimeService;
    private const int _speed = 10;
    
    public VillagerMovementView(VillagerMovement villagerMovement, NavMeshAgent navMeshAgent, GameTimeService gameTimeService)
    {
        _villagerMovement = villagerMovement;
        _navMeshAgent = navMeshAgent;
        _gameTimeService = gameTimeService;
        _villagerMovement.DestinationChanged += VillagerMovementOnDestinationChanged;
        _villagerMovement.IsMovingChanged += VillagerMovementOnIsMovingChanged;
        _gameTimeService.SpeedChanged += GameTimeServiceOnSpeedChanged;
    }

    private void VillagerMovementOnIsMovingChanged(bool isMoving)
    {
        _navMeshAgent.isStopped = isMoving;
    }

    private void VillagerMovementOnDestinationChanged(Vector3 destination, NavMeshPath navMeshPath)
    {
        _navMeshAgent.destination = destination;
        _navMeshAgent.path = navMeshPath;
    }


    public void Dispose()
    {
        _villagerMovement.DestinationChanged += VillagerMovementOnDestinationChanged;
        _villagerMovement.IsMovingChanged += VillagerMovementOnIsMovingChanged;
        _gameTimeService.SpeedChanged -= GameTimeServiceOnSpeedChanged;
    }
    
    private void GameTimeServiceOnSpeedChanged()
    {
        _navMeshAgent.speed = _speed * _gameTimeService.GameSpeed;
    }
}
