using System;

namespace Utils;

public static class RandomUtils
{
    private static readonly Random _random = new Random();
    
    public static bool CheckChance(int chance)
    {
        return _random.Next(0, chance) == 0;
    }

    public static bool CheckChance(int chance, Action onSuccess)
    {
        if (CheckChance(chance))
        {
            onSuccess();
            return true;
        }

        return false;
    }
    
    public static T GetRandomEnumItem<T>() where T : struct, Enum
    {
        var items = Enum.GetNames(typeof(T));
        var random = new Random().Next(0, items.Length);
        return Enum.Parse<T>(items[random]);
    }

    public static T GetRandomArrayItem<T>(T[] list)
    {
        var random = new Random().Next(0, list.Length);
        return list[random];
    }
}
