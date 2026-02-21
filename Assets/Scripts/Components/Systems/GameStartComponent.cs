using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Components.Systems
{
    public class GameStartComponent : MonoBehaviour
    {
        [Inject] private PrefabsService _prefabsService;
        [Inject] private GameTimeService _gameTimeService;
        
        public void Awake()
        {
            _prefabsService.SpawnVillager(new Vector3(0, 0, 0));
            _prefabsService.SpawnVillager(new Vector3(0, 1, 0));
            _gameTimeService.Play(1);
        }
    }
}
