using Services.Systems;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Services;

internal class TimerTask
{
    public double Delay { get; }
    public bool CanRun { get; set; }

    public TimerTask(double delay, bool canRun)
    {
        Delay = delay;
        CanRun = canRun;
    }
}

public delegate IEnumerator TaskCoroutine(Action task);

public class TimerService
{
    private readonly GameTimeService _gameTimeService;
    private readonly Dictionary<Action, TimerTask> _tasks = new Dictionary<Action, TimerTask>();
    public event Action<TaskCoroutine, Action> TaskRegistered;  

    public TimerService(GameTimeService gameTimeService)
    {
        _gameTimeService = gameTimeService;
    }
    
    public void Register(Action task, double delay)
    {
        _tasks.Add(task, new TimerTask(delay, true));
        TaskRegistered?.Invoke(RunTask, task);
    }

    public void Remove(Action task)
    {
        if (!_tasks.ContainsKey(task))
        {
            return;
        }
        _tasks[task].CanRun = false;
    }

    private IEnumerator RunTask(Action task)
    {
        while (_tasks[task].CanRun)
        {
            yield return _gameTimeService.Wait(_tasks[task].Delay);
            task();
        }

        _tasks.Remove(task);
    }
}
