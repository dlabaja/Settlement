using Constants;
using Models.Controllers;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class ControllerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        private InputAction forwardAction;
        private (KeyControl keyControl, Action<float> onPress)[] _keyControlsWithAction;

        private static KeyControl GetKeyControl(InputActionMap actionMap, string actionName)
        {
            return new KeyControl(actionMap.FindAction(actionName));
        }

        public void Awake()
        {
            var cameraController = new CameraController(_camera.GetComponent<Camera>());
            var _cameraMap = InputSystem.actions.FindActionMap(InputActionMapName.Camera);
            _keyControlsWithAction = new (KeyControl keyControl, Action<float> onPress)[]
            {
                (GetKeyControl(_cameraMap, InputActionName.CameraForward), cameraController.MoveForward),
                (GetKeyControl(_cameraMap, InputActionName.CameraBackward), cameraController.MoveBackward),
                (GetKeyControl(_cameraMap, InputActionName.CameraLeft), cameraController.MoveLeft),
                (GetKeyControl(_cameraMap, InputActionName.CameraRight), cameraController.MoveRight)
            };
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var (keyControl, onPress) in _keyControlsWithAction)
            {
                if (keyControl.IsPressed)
                {
                    onPress(deltaTime);
                }
            }
        }
    }
}
