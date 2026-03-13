using UnityEngine;

namespace Services;

public class CameraRaycastService
{
    public bool TryRaycast(Camera camera, Vector3 mousePosition, out RaycastHit hit, LayerMask? mask)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, mask ?? Physics.DefaultRaycastLayers);
    }

    public bool TryRaycast<T>(Camera camera, Vector3 mousePosition, out RaycastHit hit)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        if (!Physics.Raycast(ray, out hit))
        {
            return false;
        }

        return hit.transform.gameObject.TryGetComponent<T>(out _);
    }
}
