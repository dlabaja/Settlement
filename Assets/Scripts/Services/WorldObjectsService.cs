using Enums;
using Models.WorldObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services;

public class WorldObjectsService
{
    private readonly Dictionary<WorldObjectType, List<(WorldObject worldObject, GameObject gameObject)>> _objects = new Dictionary<WorldObjectType, List<(WorldObject, GameObject)>>();
    private readonly Dictionary<WorldObject, List<InteractionPoint>> _interactionPoints = new Dictionary<WorldObject, List<InteractionPoint>>();

    public void Register(WorldObjectType type, WorldObject worldObject, GameObject gameObject, List<InteractionPoint> interactionPoints)
    {
        if (!_objects.ContainsKey(type))
        {
            _objects.Add(type, new List<(WorldObject, GameObject)>());
        }
        _objects[type].Add((worldObject, gameObject));
        _interactionPoints.Add(worldObject, interactionPoints);
    }

    public void Remove(WorldObjectType type, WorldObject worldObject, GameObject gameObject)
    {
        if (!_objects.ContainsKey(type))
        {
            return;
        }

        _objects[type].Remove((worldObject, gameObject));
        _interactionPoints.Remove(worldObject);
    }

    public bool TryGetNearestObject(WorldObjectType type, Vector3 currentPos, out (WorldObject worldObject, GameObject gameObject)? output)
    {
        output = null;
        if (!_objects.TryGetValue(type, out var objects) || objects.Count == 0)
        {
            return false;
        }
        
        output = objects.OrderBy(x => Vector3.Distance(currentPos, x.gameObject.transform.position)).First();
        return true;
    }

    public bool TryGetNearestEntryPoint(WorldObject worldObject, Vector3 currentPos, out InteractionPoint interactionPoint)
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
}
