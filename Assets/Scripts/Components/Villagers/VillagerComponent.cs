using Controllers.Villagers;
using Enums;
using Factories;
using Interfaces;
using Models.Villagers;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Components.Villagers
{
    public class VillagerComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private string _name;
        [SerializeField] private Gender _gender;
        [Inject] private VillagerFactory _villagerFactory;
        [Inject] private GameTimeService _gameTimeService;
        [Inject] private GlobalInventory _globalInventory;
        private Villager _villager;
        private VillagerStatsController _villagerStatsController;
    
        public void Awake()
        {
            _villager = _villagerFactory.Create();
            _globalInventory.Register(_villager.Inventory);
            _villagerStatsController = new VillagerStatsController(_villager, _gameTimeService);
            _name = _villager.Name;
            _gender = _villager.Gender;
        }

        private void OnDestroy()
        {
            _globalInventory.Remove(_villager.Inventory);
            _villagerStatsController.Dispose();
        }
    }
}
