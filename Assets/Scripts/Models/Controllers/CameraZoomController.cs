using UnityEngine;
using Utils;

namespace Models.Controllers
{
    public class CameraZoomController
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private int _remainingTicks = maxRemainingTicks;
        private float _direction;
        private Vector3 _velocity;
        private Vector3 _startPos;
        private const int maxRemainingTicks = 200;
        private const float speedFactor = 10;

        public CameraZoomController(Transform transform, Rigidbody rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _velocity = transform.forward;
        }
        
        private void ProcessZoom(float scrollFactor, Vector3 startPos, ref Vector3 currentVelocity)
        {
            var endPos = startPos + (_transform.forward * (scrollFactor * speedFactor));
            _rigidbody.MovePosition(Vector3.SmoothDamp(_transform.position, endPos, 
                ref currentVelocity, 0.05f, 50));
        }

        public void StartZoom(float direction)
        {
            _direction = direction;
            _remainingTicks = maxRemainingTicks;
            _startPos = _transform.position;
        }

        public void Zoom()
        {
            ProcessZoom(_direction, _startPos, ref _velocity);
            _remainingTicks = VectorUtils.ApproxEql(_velocity, Vector3.zero, 0.1f) 
                ? 0 
                : _remainingTicks - 1;
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
