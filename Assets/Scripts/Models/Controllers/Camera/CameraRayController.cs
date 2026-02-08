using UnityEngine;

namespace Models.Controllers.Camera;

public class CameraRayController
{
    private readonly UnityEngine.Camera _camera;

    public CameraRayController(UnityEngine.Camera camera)
    {
        _camera = camera;
    }

    public bool TryRaycast(Vector3 mousePosition, out RaycastHit hit, LayerMask? mask)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, mask ?? Physics.DefaultRaycastLayers);
    }
}
