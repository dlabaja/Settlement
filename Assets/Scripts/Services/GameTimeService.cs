using System;

namespace Services;

public class GameTimeService
{
    public int Ticks { get; private set; }
    public int GameSpeed { get; private set; }
    public bool IsPaused => GameSpeed == 0;
    public event Action TimeTicked;

    public void Pause()
    {
        GameSpeed = 0;
    }

    public void Play(int gameSpeed)
    {
        GameSpeed = Math.Clamp(gameSpeed, 1, 3);
    }

    public void Tick()
    {
        Ticks++;
        TimeTicked?.Invoke();
    }
}
