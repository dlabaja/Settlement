using Attributes;
using Constants;
using Instances;
using Managers;
using Models.Controllers.Camera;
using Models.Controls;
using UnityEngine;
using Views;

namespace Components.Camera
{
    public class CameraSelectControllerComponent : MonoBehaviour
    {
        [Autowired] private MaterialsManager _materialsManager;
        [Autowired] private MousePositionManager _mousePositionManager;
        private CameraSelectController _cameraSelectController;
        private CameraRayController _cameraRayController;
        private CameraSelectView _cameraSelectView;
        private KeyControl _selectedKey;
        
        public void Awake()
        {
            _cameraRayController = new CameraRayController(GetComponent<UnityEngine.Camera>());
            _cameraSelectController = new CameraSelectController();
            _cameraSelectView = new CameraSelectView(_cameraSelectController, _materialsManager);
            _selectedKey = new KeyControl(InputActionMaps.Camera.FindAction(InputActionName.CameraSelect));
        }

        public void Update()
        {
            _cameraSelectView.Process(_cameraRayController, _mousePositionManager.Position, _selectedKey.WasPressedThisFrame());
        }
    }
}
