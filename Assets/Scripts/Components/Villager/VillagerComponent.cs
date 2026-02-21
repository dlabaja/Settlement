using Controllers.Villager;
using Enums;
using Factories;
using Interfaces;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Components.Villager
{
    public class VillagerComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private string _name;
        [SerializeField] private Gender _gender;
        [Inject] private VillagerFactory _villagerFactory;
        [Inject] private GameTimeService _gameTimeService;
        private Models.Villager.Villager _villager;
        private VillagerStatsController _villagerStatsController;
    
        public void Awake()
        {
            _villager = _villagerFactory.Create();
            _villagerStatsController = new VillagerStatsController(_villager, _gameTimeService);
            _name = _villager.Name;
            _gender = _villager.Gender;
        }

        private void OnDestroy()
        {
            _villagerStatsController.Dispose();
        }
    }
}
