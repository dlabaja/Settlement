using Controllers.Villagers;
using Enums;
using Factories;
using Interfaces;
using Models.Villagers;
using Reflex.Attributes;
using Services;
using Services.GameObjects;
using UnityEngine;
using UnityEngine.AI;

namespace Components.GameObjects
{
    public class VillagerComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private string _name;
        [SerializeField] private Gender _gender;
        [Inject] private VillagerFactory _villagerFactory;
        [Inject] private GameTimeService _gameTimeService;
        [Inject] private GlobalInventory _globalInventory;
        [Inject] private VillagerService _villagerService;
        [Inject] private WorldObjectsService _worldObjectsService;
        private Villager _villager;
        private VillagerMovement _villagerMovement;
        private VillagerStatsController _villagerStatsController;
        private VillagerTasksController _villagerTasksController;
    
        public void Awake()
        {
            _villager = _villagerFactory.Create();
            _globalInventory.Register(_villager.Inventory);
            _villagerService.Register(_villager, gameObject);
            _villagerMovement = new VillagerMovement(GetComponent<NavMeshAgent>(), _gameTimeService);
            _villagerStatsController = new VillagerStatsController(_villager, _gameTimeService);
            _villagerTasksController = new VillagerTasksController(_villager, _villagerMovement, gameObject, _worldObjectsService);
            _name = _villager.Name;
            _gender = _villager.Gender;
        }

        private void OnDestroy()
        {
            _globalInventory.Remove(_villager.Inventory);
            _villagerService.Remove(_villager);
            _villagerStatsController.Dispose();
        }
    }
}
