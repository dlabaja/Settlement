using Enums;
using Models.WorldObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Services;

public class WorldObjectsService
{
    private Dictionary<WorldObjectType, List<WorldObject>> _worldObjects = new Dictionary<WorldObjectType, List<WorldObject>>();
    private Dictionary<WorldObjectType, List<GameObject>> _gameObjects = new Dictionary<WorldObjectType, List<GameObject>>();

    public void Add(WorldObjectType type, WorldObject worldObject, GameObject gameObject)
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
    }

    public void Remove(WorldObjectType type, WorldObject worldObject, GameObject gameObject)
    {
        if (!_worldObjects.ContainsKey(type) || !_gameObjects.ContainsKey(type))
        {
            return;
        }

        _worldObjects[type].Remove(worldObject);
        _gameObjects[type].Remove(gameObject);
    }
}
