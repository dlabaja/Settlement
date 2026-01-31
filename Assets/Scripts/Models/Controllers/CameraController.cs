using UnityEngine;

namespace Models.Controllers
{
    public class CameraController
    {
        private readonly Camera _camera;
        private readonly Transform _transform;
        public float MoveSpeed { get; set; } = 10;

        public CameraController(Camera camera)
        {
            _camera = camera;
            _transform = _camera.transform;
        }
        
        private void Move(Vector3 vector, float deltaTime)
        {
            _camera.transform.position += vector * (MoveSpeed * deltaTime);
        }

        public void MoveForward(float deltaTime)
        {
           Move(_transform.forward, deltaTime);
        }
        
        public void MoveBackward(float deltaTime)
        {
            Move(- _transform.forward, deltaTime);
        }
        
        public void MoveRight(float deltaTime)
        {
            Move(_transform.right, deltaTime);
        }
        
        public void MoveLeft(float deltaTime)
        {
            Move(- _transform.right, deltaTime);
        }
    }
}