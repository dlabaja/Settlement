using System;

namespace Utils;

public static class RandomUtils
{
    private static readonly Random _random = new Random();

    public static int FromInterval(int start, int end)
    {
        return _random.Next(start, end + 1);
    }
    
    public static double FromIntervalF(float start, float end)
    {
        return start + (end - start) * _random.NextDouble();
    }
    
    public static double FromRangeF(float value, float range)
    {
        return (value - range) + (value + range) * _random.NextDouble();
    }
    
    public static bool GetChance(int chance)
    {
        return _random.Next(0, chance) == 0;
    }

    public static bool WhenChance(int chance, Action onSuccess)
    {
        if (GetChance(chance))
        {
            onSuccess();
            return true;
        }

        return false;
    }
    
    public static T GetRandomEnumItem<T>() where T : struct, Enum
    {
        var items = Enum.GetNames(typeof(T));
        var random = _random.Next(0, items.Length);
        return Enum.Parse<T>(items[random]);
    }

    public static T GetRandomArrayItem<T>(T[] list)
    {
        var random = _random.Next(0, list.Length);
        return list[random];
    }
}
