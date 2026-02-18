using Attributes;
using Enums;
using Factories;
using Services;
using UnityEngine;

namespace Components.WorldObject
{
    public class WorldObjectComponent : MonoBehaviour
    {
        [SerializeField] private WorldObjectType worldObjectType;
        [Autowired] private WorldObjectFactory _worldObjectFactory;
        [Autowired] private WorldObjectsService _worldObjectsService;
        private Models.WorldObjects.WorldObject _worldObject;

        public void Awake()
        {
            _worldObject = _worldObjectFactory.Create(worldObjectType);
            _worldObjectsService.Add(worldObjectType, _worldObject, gameObject);
        }

        public void OnDestroy()
        {
            _worldObjectsService.Remove(worldObjectType, _worldObject, gameObject);
        }
    }
}
