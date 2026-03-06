using Models.Villagers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.GameObjects;

public class VillagerService
{
    private readonly Dictionary<Villager, GameObject> _villagers = new Dictionary<Villager, GameObject>();
    private readonly Dictionary<GameObject, Villager> _gameObjects = new Dictionary<GameObject, Villager>();
    
    public void Register(Villager villager, GameObject gameObject)
    {
        _villagers.TryAdd(villager, gameObject);
        _gameObjects.TryAdd(gameObject, villager);
    }

    public void Remove(Villager villager)
    {
        _villagers.Remove(villager);
        var canRemove = _villagers.TryGetValue(villager, out var gameObject);
        if (canRemove)
        {
            _gameObjects.Remove(gameObject);
        }
    }

    public bool TryGetGameObject(Villager villager, out GameObject gameObject)
    {
        gameObject = null;
        return _villagers.TryGetValue(villager, out gameObject);
    }

    public bool TryGetVillager(GameObject gameObject, out Villager villager)
    {
        villager = null;
        return _gameObjects.TryGetValue(gameObject, out villager);
    }
    
    public List<Villager> Villagers
    {
        get => _villagers.Keys.ToList();
    }
}
