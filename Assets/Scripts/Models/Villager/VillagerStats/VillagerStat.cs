using System;

namespace Models.Villager.VillagerStats;

public class VillagerStat
{
    public int Value { get; private set; } = 100;

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
        ChangeValue(amount);

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

    private void ChangeValue(int ammount)
    {
        Value = Math.Clamp(Value + ammount, 0, 100);
    }
}
