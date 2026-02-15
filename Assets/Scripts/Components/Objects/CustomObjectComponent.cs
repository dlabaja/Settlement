using Attributes;
using Enums;
using Factories;
using Managers;
using Models.Objects;
using UnityEngine;

namespace Components.Objects
{
    public class CustomObjectComponent : MonoBehaviour
    {
        [SerializeField] private CustomObjectType _customObjectType;
        [Autowired] private CustomObjectFactory _customObjectFactory;
        [Autowired] private PlaceManager _placeManager;
        private CustomObject _customObject;

        public void Awake()
        {
            _customObject = _customObjectFactory.Create(_customObjectType);
            _placeManager.Add(_customObjectType, _customObject, gameObject);
        }

        public void OnDestroy()
        {
            _placeManager.Remove(_customObjectType, _customObject, gameObject);
        }
    }
}
