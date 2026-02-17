using UnityEngine;

namespace Models.Camera;

public class CameraRay
{
    public bool TryRaycast(UnityEngine.Camera camera, Vector3 mousePosition, out RaycastHit hit, LayerMask? mask)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, mask ?? Physics.DefaultRaycastLayers);
    }
}
