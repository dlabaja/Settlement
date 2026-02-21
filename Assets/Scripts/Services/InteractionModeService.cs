using Enums;
using System;

namespace Services;

public class InteractionModeService
{
    public InteractionMode InteractionMode { get; private set; } = InteractionMode.Default;
    public event Action ModeChanged;

    public void ChangeMode(InteractionMode interactionMode)
    {
        InteractionMode = interactionMode;
        ModeChanged?.Invoke();
    }
}
