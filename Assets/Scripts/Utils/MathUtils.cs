using System;

namespace Utils;

public static class MathUtils
{
    public static int Diff(this int a, int b)
    {
        return Math.Abs(a - b);
    }
        
    public static float Diff(this float a, float b)
    {
        return Math.Abs(a - b);
    }
        
    public static bool Approx(this float a, float b, float eps)
    {
        return Diff(a, b) < eps;
    }

    // inclusive
    public static bool InInterval(int num, int start, int end)
    {
        return start <= num && num <= end;
    }
}