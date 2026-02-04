using UnityEngine;

namespace Utils
{
    public static class QuaternionUtils
    {
        public static Quaternion Zero = new Quaternion(0, 0, 0, 1);

        public static Quaternion Add(Quaternion q1, Quaternion q2)
        {
            return q1 * q2;
        }

        public static Quaternion Subtract(Quaternion q1, Quaternion q2)
        {
            return q1 * Quaternion.Inverse(q2);
        }
    }
}
