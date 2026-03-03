using JetBrains.Annotations;
using Models.Villagers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.GameObjects;

public class VillagerService
{
    private readonly Dictionary<Villager, GameObject> _villagers = new Dictionary<Villager, GameObject>();
    
    public void Register(Villager villager, GameObject gameObject)
    {
        _villagers.TryAdd(villager, gameObject);
    }

    public void Remove(Villager villager)
    {
        _villagers.Remove(villager);
    }

    public bool TryGetGameObject(Villager villager, [CanBeNull] out GameObject gameObject)
    {
        gameObject = null;
        return _villagers.TryGetValue(villager, out gameObject);
    }
    
    public List<Villager> Villagers
    {
        get => _villagers.Keys.ToList();
    }
}
