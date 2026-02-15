using Attributes;
using Factories;
using Interfaces;
using Models.Villager;
using UnityEngine;

namespace Components.Objects
{
    public class VillagerComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private string _name;
        [Autowired] private VillagerFactory _villagerFactory;
        private Villager _villager;
    
        public void Start()
        {
            _villager = _villagerFactory.Create();
            _name = _villager.Name;
        }
    }
}
