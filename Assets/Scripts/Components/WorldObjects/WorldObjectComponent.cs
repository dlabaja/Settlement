using Enums;
using Factories;
using Interfaces;
using Models.WorldObjects;
using NUnit.Framework;
using Reflex.Attributes;
using Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.WorldObjects
{
    public class WorldObjectComponent : MonoBehaviour, ISelectable
    {
        [SerializeField] private WorldObjectType _worldObjectType;
        [Inject] private WorldObjectFactory _worldObjectFactory;
        [Inject] private WorldObjectsService _worldObjectsService;
        [Inject] private GlobalInventory _globalInventory;
        private WorldObject _worldObject;

        public void Awake()
        {
            _worldObject = _worldObjectFactory.Create(_worldObjectType);
            _worldObjectsService.Register(_worldObjectType, _worldObject, gameObject, GetInteractionPoints());
            _globalInventory.Register(_worldObject.Inventory);
            
        }

        public void OnDestroy()
        {
            _worldObjectsService.Remove(_worldObjectType, _worldObject, gameObject);
            _globalInventory.Remove(_worldObject.Inventory);
        }

        private List<InteractionPoint> GetInteractionPoints()
        {
            return gameObject.GetComponentsInChildren<InteractionPointComponent>()
                .Select(component =>
                {
                    component.InteractionPoint.MaxOccupantCount = InteractionPoint.GetMaxOccupantCount(_worldObjectType);
                    return component.InteractionPoint;
                })
                .ToList();
        }
    }
}
