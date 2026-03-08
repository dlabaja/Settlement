using Controllers.Systems;
using Reflex.Attributes;
using Services;
using Services.GameObjects;
using Services.Resources;
using System;
using UnityEngine;

namespace Components.Systems
{
    public class GameStartComponent : MonoBehaviour
    {
        [Inject] private PrefabsService _prefabsService;
        [Inject] private WorldObjectsService _worldObjectsService;
        [Inject] private VillagerService _villagerService;
        [Inject] private GameTimeService _gameTimeService;
        private GameStartController _gameStartController;
        private bool _lateStartCalled;
        
        public void Awake()
        {
            _gameStartController = new GameStartController();
        }

        public void Start()
        {
            _gameStartController.Init(_prefabsService);
        }

        public void FixedUpdate()
        {
            if (_lateStartCalled)
            {
                return;
            }

            _lateStartCalled = true;
            _gameStartController.Start(_worldObjectsService, _villagerService, _gameTimeService);
        }
    }
}
