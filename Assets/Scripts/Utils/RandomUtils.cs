using System;

namespace Utils;

public static class RandomUtils
{
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
