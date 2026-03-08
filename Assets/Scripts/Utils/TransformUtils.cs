using UnityEngine;

namespace Utils;

public static class TransformUtils
{
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
