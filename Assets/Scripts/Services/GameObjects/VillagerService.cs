using Models.Villagers;
using System.Collections.Generic;
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
}
