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
