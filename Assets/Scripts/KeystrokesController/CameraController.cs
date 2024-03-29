// ReSharper disable once RedundantUsingDirective

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KeystrokesController
{
    public class CameraController : KeystrokesController
    {
        [SerializeField] private float cameraDragSpeed;
        [SerializeField] private float cameraSpeed;
        [SerializeField] private float zoomSpeed;
        private InputAction _cameraDrag;
        private InputAction _cameraMovement;
        private bool _middleClickPressed;

        public void Start()
        {
            _cameraMovement = _keystrokes.Camera.Movement;
            _cameraMovement.Enable();

            _cameraDrag = _keystrokes.Camera.Drag;
            _cameraDrag.Enable();

            _cameraDrag.performed += _ => _middleClickPressed = true;
            _cameraDrag.canceled += _ => _middleClickPressed = false;
        }

        private void FixedUpdate()
        {
            CameraMovement();
            var cameraPos = transform.position;
            transform.position = new Vector3(Math.Clamp(cameraPos.x, 0, 500), cameraPos.y, Math.Clamp(cameraPos.z, 0, 500));
        }

        private void LateUpdate()
        {
            CameraZoom();
            if (_middleClickPressed)
                CameraDrag();
        }

        public void CameraZoom()
        {
            var invert = 1;
            #if !UNITY_EDITOR //linux tweaks
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                invert = -1;
            #endif
            _rigidbody.AddRelativeForce(Vector3.Normalize(Vector3.forward * (Time.deltaTime * zoomSpeed * Mouse.current.scroll.ReadValue().y * invert)),
                ForceMode.Force);
        }

        public void CameraDrag()
        {
            var x = Mouse.current.delta.x.ReadValue() * cameraDragSpeed;
            var y = Mouse.current.delta.y.ReadValue() * cameraDragSpeed;

            if (Mathf.Abs(Utils.WrapAngle(transform.eulerAngles.x - y)) > 80) y = 0;
            var eulerAngles = transform.eulerAngles;
            eulerAngles =
                Vector3.Lerp(eulerAngles, eulerAngles + new Vector3(-y, x, 0), 2f);
            transform.eulerAngles = eulerAngles;
        }

        public void CameraMovement()
        {
            var movement = _cameraMovement.ReadValue<Vector3>() * cameraSpeed;
            var direction = transform.rotation * Vector3.forward;
            _rigidbody.AddForce(Quaternion.FromToRotation(Vector3.forward,
                new Vector3(direction.x, 0, direction.z).normalized) * movement / Const.GameSpeed);
        }
    }
}
