using UnityEngine;

namespace Models.Controllers
{
    public class CameraRotationController
    {
        private readonly int speedFactor = 8;
        
        public Vector3 VectorToRotationDelta(Vector2 vector, float deltaTime)
        {
            var vector3 = new Vector3(-vector.y, vector.x, 0);
            return vector3 * (deltaTime * speedFactor);
        }
    }
}
