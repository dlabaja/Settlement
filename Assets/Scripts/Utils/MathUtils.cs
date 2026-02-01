using System;

namespace Utils
{
    public static class MathUtils
    {
        public static int Diff(int a, int b)
        {
            return Math.Abs(a - b);
        }
        
        public static float Diff(float a, float b)
        {
            return Math.Abs(a - b);
        }
        
        public static bool Approx(float a, float b, float eps)
        {
            return Diff(a, b) < eps;
        }
    }
}
