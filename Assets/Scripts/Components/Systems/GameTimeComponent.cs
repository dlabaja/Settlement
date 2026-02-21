using Controllers.Systems;
using Reflex.Attributes;
using Services;
using System.Collections;
using UnityEngine;

namespace Components.Systems
{
    public class GameTimeComponent : MonoBehaviour
    {
        [Inject] private GameTimeService _gameTimeService;
        [SerializeField] private int ticks;
        private GameTimeController _gameTimeController;
        private const float TickDelaySecs = 0.1f;
        
        public void Awake()
        {
            _gameTimeController = new GameTimeController(_gameTimeService);
            StartCoroutine(nameof(TryTick));
        }

        public void Update()
        {
            ticks = _gameTimeService.Ticks;
        }

        private IEnumerator TryTick()
        {
            while (true)
            {
                _gameTimeController.TryTick();
                yield return new WaitForSeconds(TickDelaySecs);
            }
        }
    }
}
