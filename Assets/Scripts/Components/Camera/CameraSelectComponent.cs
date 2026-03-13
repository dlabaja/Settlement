using Constants;
using Controllers.Camera;
using Instances;
using Models.Camera;
using Models.Controls;
using Reflex.Attributes;
using Services.Controls;
using Services.Resources;
using UnityEngine;
using Views.Camera;

namespace Components.Camera
{
    public class CameraSelectComponent : MonoBehaviour
    {
        [Inject] private MaterialsService _materialsService;
        [Inject] private MousePositionService _mousePositionService;
        [Inject] private CameraRaycastService _cameraRaycastService;
        private UnityEngine.Camera _camera;
        private CameraSelect _cameraSelect;
        private CameraSelectView _cameraSelectView;
        private CameraSelectController _cameraSelectController;
        private KeyControl _selectedKey;
        
        public void Awake()
        {
            _cameraSelect = new CameraSelect();
            _cameraSelectView = new CameraSelectView(_cameraSelect, _materialsService);
            _cameraSelectController = new CameraSelectController(_cameraSelect, _cameraRaycastService);
        }

        public void Start()
        {
            _camera = GetComponent<UnityEngine.Camera>();
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
