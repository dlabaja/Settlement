using Controllers.GameObjects;
using Factories;
using Models.Villagers;
using Models.WorldObjects;
using Reflex.Attributes;
using Services.GameObjects;
using Services.Resources;
using System;
using System.Collections;
using UnityEngine;

namespace Components.GameObjects
{
    public class InteractionPointComponent : MonoBehaviour
    {
        [Inject] private TerrainService _terrainService;
        [Inject] private VillagerService _villagerService;
        [Inject] private WorldObjectsService _worldObjectsService;
        [Inject] private InteractionPointFactory _interactionPointFactory;
        private InteractionPointController _interactionPointController;
        private InteractionPoint _interactionPoint;
        private WorldObject _worldObject;

        public void Awake()
        {
            _interactionPointController = new InteractionPointController();
        }

        public void Start()
        {
            _worldObject = GetWorldObject();
            _interactionPoint = _interactionPointFactory.Create(_terrainService.GroundVector3(transform.position), _worldObject.WorldObjectType);
            _worldObjectsService.RegisterInteractionPoint(_interactionPoint, _worldObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!_villagerService.TryGetVillager(other.gameObject, out var villager))
            {
                return;
            }

            StartCoroutine(OnTriggerEnterAsync(villager));
        }

        public void OnTriggerExit(Collider other)
        {
            if (!_villagerService.TryGetVillager(other.gameObject, out var villager))
            {
                return;
            }
            _interactionPoint.Leave(villager);
        }

        private IEnumerator OnTriggerEnterAsync(Villager villager)
        {
            yield return _interactionPointController.OnVillagerCollision(villager, _worldObject);
        }

        private WorldObject GetWorldObject()
        {
            return GetComponentInParent<WorldObjectComponent>().WorldObject
                   ?? throw new Exception("InteractionPoint cannot find its worldObject");
        }
    }
}
