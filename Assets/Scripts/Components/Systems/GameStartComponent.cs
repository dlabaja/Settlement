using Enums;
using Reflex.Attributes;
using Services;
using Services.GameObjects;
using Services.Resources;
using UnityEngine;

namespace Components.Systems
{
    public class GameStartComponent : MonoBehaviour
    {
        [Inject] private PrefabsService _prefabsService;
        [Inject] private GameTimeService _gameTimeService;
        [Inject] private VillagerService _villagerService;
        [Inject] private WorldObjectsService _worldObjectsService;
        
        public void Start()
        {
            _prefabsService.SpawnWorldObject(WorldObjectType.Spawn, new Vector3(10, 0, 0));
            _prefabsService.SpawnVillager(new Vector3(0, 0, 0));
            _prefabsService.SpawnVillager(new Vector3(0, 1, 0));

            var spawn = _worldObjectsService.GetFirstWorldObject(WorldObjectType.Spawn);
            foreach (var villager in _villagerService.Villagers)
            {
                villager.Places.Workplace = spawn;
            }
            _gameTimeService.Play(1);
        }
    }
}
