using Enums;
using Factories;
using Interfaces;
using Models.WorldObjects;
using Reflex.Attributes;
using Services;
using Services.GameObjects;
using UnityEngine;

namespace Components.GameObjects
{
    public class WorldObjectComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private WorldObjectType _worldObjectType;
        [Inject] private WorldObjectFactory _worldObjectFactory;
        [Inject] private WorldObjectsService _worldObjectsService;
        [Inject] private GlobalInventoryService _globalInventoryService;
        public WorldObject WorldObject { get; private set; }

        public void Awake()
        {
            WorldObject = _worldObjectFactory.Create(_worldObjectType);
        }

        public void Start()
        {
            _worldObjectsService.Register(_worldObjectType, WorldObject, gameObject);
            _globalInventoryService.Register(WorldObject.Inventory);
        }

        public void OnDestroy()
        {
            _worldObjectsService.Remove(_worldObjectType, WorldObject, gameObject);
            _globalInventoryService.Remove(WorldObject.Inventory);
        }
    }
}
