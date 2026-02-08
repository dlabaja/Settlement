using Attributes;
using Constants;
using Managers;
using Models.Controllers.Camera;
using Models.Controls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Camera
{
    public class CameraSelectControllerComponent : MonoBehaviour
    {
        [Autowired] private MaterialsManager _materialsManager;
        [Autowired] private MousePositionManager _mousePositionManager;
        private CameraSelectController _cameraSelectController;
        private CameraRayController _cameraRayController;
        private Renderer CurrentHighlighted;
        private Renderer CurrentSelected;
        private KeyControl _selectedKey;
    
        public void Start()
        {
            _cameraRayController = new CameraRayController(GetComponent<UnityEngine.Camera>());
            _cameraSelectController = new CameraSelectController(_materialsManager);
            _selectedKey = new KeyControl(new InputAction(InputActionName.CameraSelect));
        }

        public void Update()
        {
            var hit = _cameraRayController.TryRaycast(_mousePositionManager.Position, out var raycastedObj);
            if (!hit)
            {
                return;
            }
        
            ManageHighlight(raycastedObj.transform.gameObject);
            if (_selectedKey.WasPressedThisFrame())
            {
                ManageSelect(raycastedObj.transform.gameObject);
            }
        }

        private void ManageHighlight(GameObject raycasted)
        {
            Manage(raycasted, ref CurrentHighlighted, _cameraSelectController.Highlight);
        }

        private void ManageSelect(GameObject raycasted)
        {
            Manage(raycasted, ref CurrentSelected, _cameraSelectController.Select);
        }

        private void Manage(GameObject raycasted, ref Renderer currentRenderer, Action<Renderer> func)
        {
            if (raycasted == currentRenderer.gameObject)
            {
                return;
            }

            var _renderer = raycasted.GetComponent<Renderer>();
            _cameraSelectController.Reset(currentRenderer);
            func(_renderer);
            currentRenderer = _renderer;
        }
    }
}
