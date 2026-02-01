using UnityEngine;

namespace Models.Controllers
{
    public class CameraZoomController
    {
        private readonly Transform _transform;
        private int _remainingTicks = maxRemainingTicks;
        private float _direction;
        private const int maxRemainingTicks = 20;
        private const float speedFactor = 40;

        public CameraZoomController(Transform transform)
        {
            _transform = transform;
        }

        public void StartZoom(float direction)
        {
            _direction = direction;
            _remainingTicks = maxRemainingTicks;
        }

        public Vector3 ZoomedVectorDelta(float deltaTime)
        {
            if (ZoomEnded)
            {
                return Vector3.zero;
            }
            _remainingTicks--;
            return _transform.forward * (_direction * speedFactor * deltaTime);
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
