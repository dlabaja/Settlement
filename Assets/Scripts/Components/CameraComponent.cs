using Constants;
using Models.Controllers;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class CameraComponent : MonoBehaviour
    {
        private CameraController _cameraController;
        private InputActionMap _actionMap;
        private InputAction forwardAction;
        private (KeyControl keyControl, Action<float> onPress)[] _keyControlsWithAction;

        private KeyControl GetKeyControl(string actionName)
        {
            return new KeyControl(_actionMap.FindAction(actionName));
        }

        public void Awake()
        {
            _cameraController = new CameraController(GetComponent<Camera>());
            _actionMap = InputSystem.actions.FindActionMap("Camera");
            _keyControlsWithAction = new (KeyControl keyControl, Action<float> onPress)[]
            {
                (GetKeyControl(InputActionName.CameraForward), _cameraController.MoveForward),
                (GetKeyControl(InputActionName.CameraBackward), _cameraController.MoveBackward),
                (GetKeyControl(InputActionName.CameraLeft), _cameraController.MoveLeft),
                (GetKeyControl(InputActionName.CameraRight), _cameraController.MoveRight)
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
