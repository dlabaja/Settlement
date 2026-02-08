using UnityEngine;

namespace Models.Controllers.Camera;

public class CameraRayController
{
    private readonly UnityEngine.Camera _camera;

    public CameraRayController(UnityEngine.Camera camera)
    {
        _camera = camera;
    }

    public bool TryRaycast(Vector3 mousePosition, out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit);
    }
}
