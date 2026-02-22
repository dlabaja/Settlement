using Enums;
using Reflex.Core;
using Reflex.Injectors;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services;

public class PrefabsService
{
    private readonly Dictionary<WorldObjectType, GameObject> _worldObjectPrefabs;
    private readonly GameObject _villagerPrefab;

    public PrefabsService(GameObject[] worldObjectPrefabs, GameObject villagerPrefab)
    {
        _worldObjectPrefabs = BindPrefabsToTypes(worldObjectPrefabs);
        _villagerPrefab = villagerPrefab;
    }

    public void SpawnVillager(Vector3 position)
    {
        Instantiate(_villagerPrefab, position);
    }

    public void SpawnWorldObject(WorldObjectType type, Vector3 position, Quaternion? quaternion = null)
    {
        Instantiate(GetWorldObject(type), position, quaternion ?? Quaternion.identity);
    }

    private void Instantiate(GameObject gameObject, Vector3 position, Quaternion? quaternion = null)
    {
        gameObject.SetActive(false);
        var obj = UnityEngine.Object.Instantiate(gameObject, position, quaternion ?? Quaternion.identity);
        GameObjectInjector.InjectRecursive(obj, Container.RootContainer);
        obj.SetActive(true);
    }

    public static GameObject[] LoadAllPrefabs()
    {
        return Resources.LoadAll<GameObject>("Prefabs");
    }

    private GameObject GetWorldObject(WorldObjectType type)
    {
        var exists = _worldObjectPrefabs.TryGetValue(type, out var gameObject);
        return !exists ? throw new Exception($"Prefab with type {type} not initialized") : gameObject;
    }

    private static Dictionary<WorldObjectType, GameObject> BindPrefabsToTypes(GameObject[] worldObjectPrefabs)
    {
        var result = new Dictionary<WorldObjectType, GameObject>();
        foreach (var prefab in worldObjectPrefabs)
        {
            var nameValid = Enum.TryParse(prefab.name, out WorldObjectType type);
            if (!nameValid)
            {
                throw new Exception($"Cannot bind {prefab.name} prefab to enum");
            }

            result.Add(type, prefab);
        }

        return result;
    }
}
