using Interfaces;
using Models.Camera;
using UnityEngine;

namespace Controllers.Camera;

public class CameraSelectController
{
    private readonly CameraRay _cameraRay;
    private readonly CameraSelect _cameraSelect;

    public CameraSelectController(CameraSelect cameraSelect)
    {
        _cameraRay = new CameraRay();
        _cameraSelect = cameraSelect;
    }

    public void UpdateRaycast(UnityEngine.Camera camera, Vector3 mousePosition, bool selectPressed)
    {
        var hit = _cameraRay.TryRaycast<ISelectable>(camera, mousePosition, out var raycastedObj);
        if (hit)
        {
            ProcessHit(raycastedObj.transform.gameObject, selectPressed);
            return;
        }
        ProcessMiss(selectPressed);
    }

    private void ProcessHit(GameObject hitObj, bool selectPressed)
    {
        if (selectPressed)
        {
            _cameraSelect.Select(hitObj);
        }
        _cameraSelect.Highlight(hitObj);
    }

    private void ProcessMiss(bool selectPressed)
    {
        if (selectPressed)
        {
            _cameraSelect.ResetSelect();
        }
        _cameraSelect.ResetHighlight();
    }
}
