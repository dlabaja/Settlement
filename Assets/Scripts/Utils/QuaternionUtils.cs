using UnityEngine;

namespace Utils
{
    public static class QuaternionUtils
    {
        public static Quaternion Add(this Quaternion q1, Quaternion q2)
        {
            return q1 * q2;
        }

        public static Quaternion Subtract(this Quaternion q1, Quaternion q2)
        {
            return q1 * Quaternion.Inverse(q2);
        }
    }
}
