using Interfaces;
using Models.Camera;
using Services.Controls;
using UnityEngine;

namespace Controllers.Camera;

public class CameraSelectController
{
    private readonly CameraRaycastService _cameraRaycastService;
    private readonly CameraSelect _cameraSelect;

    public CameraSelectController(CameraSelect cameraSelect, CameraRaycastService cameraRaycastService)
    {
        _cameraSelect = cameraSelect;
        _cameraRaycastService = cameraRaycastService;
    }

    public void UpdateRaycast(UnityEngine.Camera camera, Vector3 mousePosition, bool selectPressed)
    {
        var hit = _cameraRaycastService.TryRaycast<ISelectable>(camera, mousePosition, out var raycastedObj);
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
