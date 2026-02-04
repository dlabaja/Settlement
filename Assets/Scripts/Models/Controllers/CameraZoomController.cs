using UnityEngine;

namespace Models.Controllers
{
    public class CameraZoomController
    {
        private int _remainingTicks = maxRemainingTicks;
        private float _direction;
        private const int maxRemainingTicks = 20;
        private const float speedFactor = 40;

        public void StartZoom(float direction)
        {
            _direction = direction;
            _remainingTicks = maxRemainingTicks;
        }

        public Vector3 ZoomedVectorDelta(Vector3 forward, float deltaTime)
        {
            if (ZoomEnded)
            {
                return Vector3.zero;
            }
            _remainingTicks--;
            return forward * (_direction * speedFactor * deltaTime);
        }

        public bool ZoomEnded
        {
            get => _remainingTicks == 0;
        }

        public void StopZoom()
        {
            _remainingTicks = 0;
        }
    }
}
