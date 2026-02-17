using Attributes;
using Enums;
using Factories;
using Interfaces;
using UnityEngine;

namespace Components.Villager
{
    public class VillagerComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private string _name;
        [SerializeField] private Gender _gender;
        [Autowired] private VillagerFactory _villagerFactory;
        private Models.Villager.Villager _villager;
    
        public void Start()
        {
            _villager = _villagerFactory.Create();
            _name = _villager.Name;
            _gender = _villager.Gender;
        }
    }
}
