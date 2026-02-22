using Enums;
using Models.WorldObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services;

public class WorldObjectsService
{
    private readonly Dictionary<WorldObjectType, List<WorldObject>> _worldObjects = new Dictionary<WorldObjectType, List<WorldObject>>();
    private readonly Dictionary<WorldObjectType, List<GameObject>> _gameObjects = new Dictionary<WorldObjectType, List<GameObject>>();
    private readonly Dictionary<GameObject, List<InteractionPoint>> _interactionPoints = new Dictionary<GameObject, List<InteractionPoint>>();

    public void Register(WorldObjectType type, WorldObject worldObject, GameObject gameObject, List<InteractionPoint> interactionPoints)
    {
        if (!_worldObjects.ContainsKey(type))
        {
            _worldObjects.Add(type, new List<WorldObject>());
        }
        _worldObjects[type].Add(worldObject);

        if (!_gameObjects.ContainsKey(type))
        {
            _gameObjects.Add(type, new List<GameObject>());
        }
        _gameObjects[type].Add(gameObject);
        
        _interactionPoints.Add(gameObject, interactionPoints);
    }

    public void Remove(WorldObjectType type, WorldObject worldObject, GameObject gameObject)
    {
        if (!_worldObjects.ContainsKey(type) || !_gameObjects.ContainsKey(type))
        {
            return;
        }

        _worldObjects[type].Remove(worldObject);
        _gameObjects[type].Remove(gameObject);
        _interactionPoints.Remove(gameObject);
    }

    public bool TryGetNearestObject(WorldObjectType type, Vector3 currentPos, out GameObject gameObject)
    {
        gameObject = null;
        if (!_gameObjects.TryGetValue(type, out var gameObjects) || gameObjects.Count == 0)
        {
            return false;
        }
        
        gameObject = gameObjects.OrderBy(x => Vector3.Distance(currentPos, x.transform.position)).First();
        return true;
    }

    public bool TryGetNearestEntryPoint(GameObject gameObject, Vector3 currentPos, out InteractionPoint interactionPoint)
    {
        interactionPoint = null;
        if (!_interactionPoints.TryGetValue(gameObject, out var interactionPoints) || interactionPoints.Count == 0)
        {
            return false;
        }

        var selectedInteractionPoints = interactionPoints.Where(x => !x.IsFull).OrderBy(x => Vector3.Distance(currentPos, x.Position));
        if (!selectedInteractionPoints.Any())
        {
            return false;
        }

        interactionPoint = selectedInteractionPoints.First();
        return true;
    }
}
