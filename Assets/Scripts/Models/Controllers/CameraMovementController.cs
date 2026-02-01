using UnityEngine;

namespace Models.Controllers
{
    public class CameraMovementController
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
        public float MoveSpeed { get; set; } = 10;

        public CameraMovementController(Transform transform, Rigidbody rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }
        
        public void Move(Vector3 vector, float deltaTime)
        {
            vector.Scale(_planeLockVector);
            _rigidbody.MovePosition(_transform.position + vector * (MoveSpeed * deltaTime));
        }

        public Vector3 Forward()
        {
           return _transform.forward;
        }
        
        public Vector3 Backward()
        {
            return -_transform.forward;
        }
        
        public Vector3 Right()
        {
            return _transform.right;
        }
        
        public Vector3 Left()
        {
            return -_transform.right;
        }
    }
}