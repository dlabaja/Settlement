using Reflex.Attributes;
using Services;
using Services.Systems;
using System;
using UnityEngine;

namespace Components.Systems
{
    public class TimerServiceComponent : MonoBehaviour
    {
        [Inject] private GameTimeService _gameTimeService;
        [Inject] private TimerService _timerService;
        
        private void Awake()
        {
            _timerService.TaskRegistered += TimerServiceOnTaskRegistered;
        }

        private void TimerServiceOnTaskRegistered(TaskCoroutine fn, Action task)
        {
            StartCoroutine(fn(task));
        }

        private void OnDestroy()
        {
            _timerService.TaskRegistered -= TimerServiceOnTaskRegistered;
        }
    }
}
