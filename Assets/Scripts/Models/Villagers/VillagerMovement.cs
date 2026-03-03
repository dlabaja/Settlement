using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Models.Villagers;

public class VillagerMovement
{
    private bool _isMoving;
    public Vector3 Destination { get; private set; }
    public event Action<bool> IsMovingChanged;
    public event Action<Vector3, NavMeshPath?> DestinationChanged;
    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            IsMovingChanged?.Invoke(value);
        }
    }

    public void SetDestination(Vector3 destination, [CanBeNull] NavMeshPath navMeshPath)
    {
        Destination = destination;
        DestinationChanged?.Invoke(destination, navMeshPath);
    }
}
