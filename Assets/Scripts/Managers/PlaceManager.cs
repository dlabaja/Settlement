using Enums;
using Models.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Managers;

public class PlaceManager
{
    private Dictionary<CustomObjectType, List<CustomObject>> _customObjects = new Dictionary<CustomObjectType, List<CustomObject>>();
    private Dictionary<CustomObjectType, List<GameObject>> _gameObjects = new Dictionary<CustomObjectType, List<GameObject>>();

    public void Add(CustomObjectType type, CustomObject customObject, GameObject gameObject)
    {
        if (!_customObjects.ContainsKey(type))
        {
            _customObjects.Add(type, new List<CustomObject>());
        }
        _customObjects[type].Add(customObject);

        if (!_gameObjects.ContainsKey(type))
        {
            _gameObjects.Add(type, new List<GameObject>());
        }
        _gameObjects[type].Add(gameObject);
    }

    public void Remove(CustomObjectType type, CustomObject customObject, GameObject gameObject)
    {
        if (!_customObjects.ContainsKey(type) || !_gameObjects.ContainsKey(type))
        {
            return;
        }

        _customObjects[type].Remove(customObject);
        _gameObjects[type].Remove(gameObject);
    }
}
