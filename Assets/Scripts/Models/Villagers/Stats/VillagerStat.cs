using System;

namespace Models.Villagers.Stats;

public class VillagerStat
{
    public int Value { get; private set; } = 100;
    public int StatLowThreshold { get; }
    public int UrgentThreshold { get; }
    public event Action StatLowThresholdReached; // he should start to look for it
    public event Action UrgentThresholdReached; // he will die/leave

    public VillagerStat(int statLowTreshold, int urgentThreshold)
    {
        StatLowThreshold = statLowTreshold;
        UrgentThreshold = urgentThreshold;
    }

    public void Reset()
    {
        Value = 100;
    }

    public void Decrease()
    {
        ChangeValue(-1);
    }

    public void Decrease(int amount)
    {
        ChangeValue(-amount);
    }
    
    public void Increase()
    {
        ChangeValue(1);
    }

    public void Increase(int amount)
    {
        ChangeValue(amount);
    }
    
    public bool IsEmpty
    {
        get => Value == 0;
    }

    private void ChangeValue(int amount)
    {
        Value = Math.Clamp(Value + amount, 0, 100);
        if (Value <= UrgentThreshold)
        {
            UrgentThresholdReached?.Invoke();
            return;
        }
        if (Value <= StatLowThreshold)
        {
            StatLowThresholdReached?.Invoke();
        }
    }
}
