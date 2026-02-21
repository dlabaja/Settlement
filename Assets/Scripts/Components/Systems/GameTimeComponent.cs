using Controllers.Systems;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Components.Systems
{
    public class GameTimeComponent : MonoBehaviour
    {
        [Inject] private GameTimeService _gameTimeService;
        [SerializeField] private int ticks;
        private GameTimeController _gameTimeController;
        
        public void Awake()
        {
            _gameTimeController = new GameTimeController(_gameTimeService);
        }

        public void FixedUpdate()
        {
            _gameTimeController.TryTick();
            ticks = _gameTimeService.Ticks;
        }
    }
}
