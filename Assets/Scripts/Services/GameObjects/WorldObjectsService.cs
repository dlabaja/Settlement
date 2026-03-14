using Enums;
using JetBrains.Annotations;
using Models.WorldObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.GameObjects;

public class WorldObjectsService
{
    private readonly Dictionary<WorldObjectType, List<WorldObject>> _types = new Dictionary<WorldObjectType, List<WorldObject>>();
    private readonly Dictionary<WorldObject, GameObject> _objects = new Dictionary<WorldObject, GameObject>();
    private readonly Dictionary<WorldObject, List<InteractionPoint>> _interactionPoints = new Dictionary<WorldObject, List<InteractionPoint>>();

    public void Register(WorldObjectType type, WorldObject worldObject, GameObject gameObject)
    {
        if (!_types.ContainsKey(type))
        {
            _types.Add(type, new List<WorldObject>());
        }
        _types[type].Add(worldObject);
        _objects.Add(worldObject, gameObject);
    }

    public void RegisterInteractionPoint(InteractionPoint interactionPoint, WorldObject worldObject)
    {
        if (!_interactionPoints.ContainsKey(worldObject))
        {
            _interactionPoints.Add(worldObject, new List<InteractionPoint>());
        }
        _interactionPoints[worldObject].Add(interactionPoint);
    }

    public void Remove(WorldObjectType type, WorldObject worldObject, GameObject gameObject)
    {
        if (!_types.ContainsKey(type))
        {
            return;
        }

        _types[type].Remove(worldObject);
        _objects.Remove(worldObject);
        _interactionPoints.Remove(worldObject);
    }

    public bool TryGetGameObject(WorldObject worldObject, out GameObject gameObject)
    {
        gameObject = null;
        if (_objects.ContainsKey(worldObject))
        {
            gameObject = _objects[worldObject];
            return true;
        }

        return false;
    }

    public bool TryGetNearestObject(WorldObjectType type, Vector3 currentPos, out WorldObject worldObject)
    {
        worldObject = null;
        if (!_types.TryGetValue(type, out var worldObjects) || worldObjects.Count == 0)
        {
            return false;
        }
        
        var closestWO = worldObjects.OrderBy(wo => Vector3.Distance(currentPos, _objects[wo].transform.position)).First();
        worldObject = closestWO;
        return true;
    }

    public bool TryGetNearestInteractionPoint(WorldObject worldObject, Vector3 currentPos, out InteractionPoint interactionPoint)
    {
        interactionPoint = null;
        if (!_interactionPoints.TryGetValue(worldObject, out var interactionPoints) || interactionPoints.Count == 0)
        {
            return false;
        }

        var selectedInteractionPoints = interactionPoints
            .Where(x => !x.IsFull)
            .OrderBy(x => Vector3.Distance(currentPos, x.Position))
            .ToArray();
        if (!selectedInteractionPoints.Any())
        {
            return false;
        }

        interactionPoint = selectedInteractionPoints.First();
        return true;
    }
    
    public List<WorldObject> GetWorldObjects(WorldObjectType type)
    {
        return _types[type].ToList();
    }
    
    [CanBeNull]
    public WorldObject GetFirstWorldObject(WorldObjectType type)
    {
        return _types[type].FirstOrDefault();
    }
}
