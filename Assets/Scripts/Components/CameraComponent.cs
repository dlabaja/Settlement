using Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class CameraComponent : MonoBehaviour
    {
        private CameraController _cameraController;
        private InputActionMap actionMap;
        private InputAction forwardAction;
        private bool isMovingForward;

        public void Awake()
        {
            _cameraController = new CameraController(GetComponent<Camera>());
            actionMap = InputSystem.actions.FindActionMap("Camera");
            forwardAction = actionMap.FindAction("Forward");
        }

        public void Update()
        {
            Logging.Log(isMovingForward);
            if (forwardAction.WasPressedThisFrame())
            {
                isMovingForward = true;
            }

            if (forwardAction.WasReleasedThisFrame())
            {
                isMovingForward = false;
            }
            if (isMovingForward)
            {
                Logging.Log("ss");
                _cameraController.MoveForward(Time.deltaTime);
            }
        }
    }
}
