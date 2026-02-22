using System;

namespace Services;

public class GameTimeService
{
    public int Ticks { get; private set; }
    public int GameSpeed { get; private set; }
    public readonly int MaxSpeed = 3;
    public bool IsPaused => GameSpeed == 0;
    public event Action TimeTicked;
    public event Action SpeedChanged;

    public void Pause()
    {
        GameSpeed = 0;
        SpeedChanged?.Invoke();
    }

    public void Play(int gameSpeed)
    {
        GameSpeed = Math.Clamp(gameSpeed, 1, MaxSpeed);
        SpeedChanged?.Invoke();
    }

    public void Tick()
    {
        Ticks++;
        TimeTicked?.Invoke();
    }
}
