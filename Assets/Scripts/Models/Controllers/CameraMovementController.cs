using UnityEngine;

namespace Models.Controllers
{
    public class CameraMovementController
    {
        private readonly Transform _transform;
        private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
        public float MoveSpeed { get; set; } = 10;

        public CameraMovementController(Transform transform)
        {
            _transform = transform;
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
    }
}