using Controllers.GameObjects;
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
        private InteractionPointController _interactionPointController;
        private InteractionPoint _interactionPoint;
        private WorldObject _worldObject;

        public void Awake()
        {
            _interactionPointController = new InteractionPointController();
            _interactionPoint = _interactionPointController.GetGrounded(transform.position, _terrainService);
        }

        public void Start()
        {
            _worldObject = GetWorldObject();
            _worldObjectsService.RegisterInteractionPoint(_interactionPoint, _worldObject);
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!_villagerService.TryGetVillager(other.gameObject, out var villager))
            {
                return;
            }

            StartCoroutine(OnCollisionEnterAsync(villager));
        }

        private IEnumerator OnCollisionEnterAsync(Villager villager)
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
