using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        private static bool DiffLt(float a, float b, float eps)
        {
            return MathUtils.Diff(a, b) < eps;
        }
        
        public static bool ApproxEql(Vector3 vec1, Vector3 vec2, float eps)
        {
            return DiffLt(vec1.x, vec2.x, eps) && DiffLt(vec1.y, vec2.y, eps) && DiffLt(vec1.z, vec2.z, eps);
        }
    }
}
