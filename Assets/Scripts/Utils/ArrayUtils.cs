using System;

namespace Utils;

public static class ArrayUtils
{
    public static T[] CreateFilled<T>(int length, Func<T> generator)
    {
        var array = new T[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = generator();
        }

        return array;
    }
}
