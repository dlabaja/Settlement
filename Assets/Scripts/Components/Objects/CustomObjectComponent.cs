using Attributes;
using Enums;
using Factories;
using Models.Objects;
using Services;
using UnityEngine;

namespace Components.Objects
{
    public class CustomObjectComponent : MonoBehaviour
    {
        [SerializeField] private CustomObjectType _customObjectType;
        [Autowired] private CustomObjectFactory _customObjectFactory;
        [Autowired] private PlaceService _placeService;
        private CustomObject _customObject;

        public void Awake()
        {
            _customObject = _customObjectFactory.Create(_customObjectType);
            _placeService.Add(_customObjectType, _customObject, gameObject);
        }

        public void OnDestroy()
        {
            _placeService.Remove(_customObjectType, _customObject, gameObject);
        }
    }
}
