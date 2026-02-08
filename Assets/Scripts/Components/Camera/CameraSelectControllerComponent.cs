using Attributes;
using Constants;
using Instances;
using Interfaces;
using JetBrains.Annotations;
using Managers;
using Models.Controllers.Camera;
using Models.Controls;
using Models.Objects;
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
        private KeyControl _selectedKey;
        private LayerMask _selectableLayerMask;
        
        public void Awake()
        {
            _cameraRayController = new CameraRayController(GetComponent<UnityEngine.Camera>());
            _cameraSelectController = new CameraSelectController(_materialsManager);
            _selectedKey = new KeyControl(InputActionMaps.Camera.FindAction(InputActionName.CameraSelect));
            _selectableLayerMask = LayerMask.GetMask(PhysicsLayer.Selectable);
        }

        public void Update()
        {
            var hit = _cameraRayController.TryRaycast(_mousePositionManager.Position, out var raycastedObj, _selectableLayerMask);
            if (hit)
            {
                _cameraSelectController.Highlight(raycastedObj.transform.gameObject);
            }
            else if (_cameraSelectController.Highlighted)
            {
                _cameraSelectController.ResetHighlight();
            }
        }
    }
}
