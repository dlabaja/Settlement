using Controllers.Villagers;
using Enums;
using Factories;
using Interfaces;
using Models.Villagers;
using Reflex.Attributes;
using Services;
using Services.GameObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Views.Villagers;

namespace Components.GameObjects
{
    public class VillagerComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private string _name;
        [SerializeField] private Gender _gender;
        [Inject] private VillagerFactory _villagerFactory;
        [Inject] private VillagerTaskFactory _villagerTaskFactory;
        [Inject] private GameTimeService _gameTimeService;
        [Inject] private GlobalInventoryService _globalInventoryService;
        [Inject] private VillagerService _villagerService;
        [Inject] private WorldObjectsService _worldObjectsService;
        [Inject] private PathfindingService _pathfindingService;
        private Villager _villager;
        private VillagerMovement _villagerMovement;
        private VillagerStatsController _villagerStatsController;
        private VillagerTasksController _villagerTasksController;
        private VillagerMovementView _villagerMovementView;

        public void Awake()
        {
            _villager = _villagerFactory.Create();
            _villagerMovement = new VillagerMovement();
            
            _villagerStatsController = new VillagerStatsController(_villager, _gameTimeService);
            _villagerTasksController = new VillagerTasksController(_villager, _villagerMovement, _worldObjectsService, _pathfindingService, _villagerTaskFactory);

            _villagerMovementView = new VillagerMovementView(_villagerMovement, GetComponent<NavMeshAgent>(), _gameTimeService);
            
            _name = _villager.Name;
            _gender = _villager.Gender;
        }

        public void Start()
        {
            _globalInventoryService.Register(_villager.Inventory);
            _villagerService.Register(_villager, gameObject);
        }

        public void Update()
        {
            StartCoroutine(UpdateTasks());
        }

        private void OnDestroy()
        {
            _globalInventoryService.Remove(_villager.Inventory);
            _villagerService.Remove(_villager);
            
            _villagerStatsController.Dispose();
            _villagerMovementView.Dispose();
        }
        
        private IEnumerator UpdateTasks()
        {
            while (true)
            {
                _villagerTasksController.ProcessTask(transform.position);
                yield return new WaitForSeconds(5);
            }
        }
    }
}
