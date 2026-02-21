using Enums;
using Factories;
using Interfaces;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Components.WorldObject
{
    public class WorldObjectComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private WorldObjectType worldObjectType;
        [Inject] private WorldObjectFactory _worldObjectFactory;
        [Inject] private WorldObjectsService _worldObjectsService;
        [Inject] private GlobalInventory _globalInventory;
        private Models.WorldObjects.WorldObject _worldObject;

        public void Awake()
        {
            _worldObject = _worldObjectFactory.Create(worldObjectType);
            _worldObjectsService.Register(worldObjectType, _worldObject, gameObject);
            _globalInventory.Register(_worldObject.Inventory);
            
        }

        public void OnDestroy()
        {
            _worldObjectsService.Remove(worldObjectType, _worldObject, gameObject);
            _globalInventory.Remove(_worldObject.Inventory);
        }
    }
}
