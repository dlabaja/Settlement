using UnityEngine;

namespace Models.Controllers
{
    public class CameraController
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
        public float MoveSpeed { get; set; } = 10;
        public float ZoomFactor { get; set; } = 10;

        public CameraController(Camera camera, Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _transform = camera.transform;
        }
        
        private void MoveOnPlane(Vector3 vector, float deltaTime)
        {
            vector.Scale(_planeLockVector);
            _transform.position += vector * (MoveSpeed * deltaTime);
        }

        public void MoveForward(float deltaTime)
        {
           MoveOnPlane(_transform.forward, deltaTime);
        }
        
        public void MoveBackward(float deltaTime)
        {
            MoveOnPlane(- _transform.forward, deltaTime);
        }
        
        public void MoveRight(float deltaTime)
        {
            MoveOnPlane(_transform.right, deltaTime);
        }
        
        public void MoveLeft(float deltaTime)
        {
            MoveOnPlane(- _transform.right, deltaTime);
        }

        public void Zoom(float scrollFactor, Vector3 startPos, ref Vector3 currentVelocity)
        {
            var endPos = startPos + (_transform.forward * (scrollFactor * ZoomFactor));
            _rigidbody.MovePosition(Vector3.SmoothDamp(_transform.position, endPos, ref currentVelocity, 0.05f));
            Debug.Log(currentVelocity);
        }
    }
}