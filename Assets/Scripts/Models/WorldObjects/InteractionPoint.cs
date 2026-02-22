using Enums;
using Models.Villagers;
using System.Collections.Generic;
using UnityEngine;

namespace Models.WorldObjects;

public class InteractionPoint
{
    public Vector3 Position { get; }
    private readonly List<Villager> _currentOccupants = new List<Villager>();
    public int MaxOccupantCount { get; set; }
    public int CurrentOccupantCount { get; private set; }
    public bool IsFull => CurrentOccupantCount == MaxOccupantCount;

    public InteractionPoint(Vector3 position, int maxOccupantCount = 1)
    {
        Position = position;
        MaxOccupantCount = maxOccupantCount;
    }

    public bool TrySetOccupant(Villager occupant)
    {
        if (IsFull)
        {
            return false;
        }

        CurrentOccupantCount++;
        _currentOccupants.Add(occupant);
        return true;
    }

    public void Leave(Villager occupant)
    {
        if (!_currentOccupants.Contains(occupant))
        {
            return;
        }

        _currentOccupants.Remove(occupant);
        CurrentOccupantCount--;
    }
    
    public static int GetMaxOccupantCount(WorldObjectType type)
    {
        return type switch
        {
            WorldObjectType.Church => 40,
            WorldObjectType.House => 4,
            _ => 1
        };
    }
}
