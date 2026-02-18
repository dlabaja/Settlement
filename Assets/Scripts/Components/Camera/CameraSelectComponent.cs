using Constants;
using Controllers.Camera;
using Instances;
using Models.Camera;
using Models.Controls;
using Reflex.Attributes;
using Services;
using UnityEngine;
using Views.Camera;

namespace Components.Camera
{
    public class CameraSelectComponent : MonoBehaviour
    {
        [Inject] private MaterialsService _materialsService;
        [Inject] private MousePositionService _mousePositionService;
        private CameraSelect _cameraSelect;
        private CameraSelectView _cameraSelectView;
        private CameraSelectController _cameraSelectController;
        private KeyControl _selectedKey;
        private UnityEngine.Camera _camera;
        
        public void Awake()
        {
            _cameraSelect = new CameraSelect();
            _camera = GetComponent<UnityEngine.Camera>();
            _cameraSelectView = new CameraSelectView(_cameraSelect, _materialsService);
            _cameraSelectController = new CameraSelectController(_cameraSelect);
            _selectedKey = new KeyControl(InputActionMaps.Camera.FindAction(InputActionName.CameraSelect));
        }

        public void Update()
        {
            _cameraSelectController.UpdateRaycast(_camera, _mousePositionService.Position, _selectedKey.WasPressedThisFrame());
        }

        private void OnDestroy()
        {
            _cameraSelectView.Dispose();
        }
    }
}
