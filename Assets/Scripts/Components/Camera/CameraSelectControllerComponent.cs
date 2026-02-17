using Attributes;
using Constants;
using Instances;
using Managers;
using Models.Camera;
using Models.Controls;
using System;
using UnityEngine;
using Views.Camera;

namespace Components.Camera
{
    public class CameraSelectControllerComponent : MonoBehaviour
    {
        [Autowired] private MaterialsManager _materialsManager;
        [Autowired] private MousePositionManager _mousePositionManager;
        private CameraSelect _cameraSelect;
        private CameraRay _cameraRay;
        private CameraSelectView _cameraSelectView;
        private KeyControl _selectedKey;
        
        public void Awake()
        {
            _cameraRay = new CameraRay();
            _cameraSelect = new CameraSelect();
            _cameraSelectView = new CameraSelectView(GetComponent<UnityEngine.Camera>(), _cameraSelect, _materialsManager);
            _selectedKey = new KeyControl(InputActionMaps.Camera.FindAction(InputActionName.CameraSelect));
        }

        public void Update()
        {
            _cameraSelectView.Process(_cameraRay, _mousePositionManager.Position, _selectedKey.WasPressedThisFrame());
        }

        private void OnDestroy()
        {
            _cameraSelectView.Dispose();
        }
    }
}
