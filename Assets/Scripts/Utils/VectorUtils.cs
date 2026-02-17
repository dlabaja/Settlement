using UnityEngine;

namespace Utils;

public static class VectorUtils
{
    public static bool Approx(this Vector3 vec1, Vector3 vec2, float eps)
    {
        return vec1.x.Approx(vec2.x, eps) 
               && vec1.y.Approx(vec2.y, eps) 
               && vec1.z.Approx(vec2.z, eps);
    }
    
    public static Vector3 Forward(this Transform transform)
    {
        return transform.forward;
    }

    public static Vector3 Backward(this Transform transform)
    {
        return -transform.forward;
    }

    public static Vector3 Right(this Transform transform)
    {
        return transform.right;
    }

    public static Vector3 Left(this Transform transform)
    {
        return -transform.right;
    }
}