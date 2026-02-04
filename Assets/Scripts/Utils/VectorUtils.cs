using UnityEngine;

namespace Utils
{
    public static class VectorUtils
    {
        public static bool Approx(this Vector3 vec1, Vector3 vec2, float eps)
        {
            return MathUtils.Approx(vec1.x, vec2.x, eps) 
                   && MathUtils.Approx(vec1.y, vec2.y, eps) 
                   && MathUtils.Approx(vec1.z, vec2.z, eps);
        }
    }
}
