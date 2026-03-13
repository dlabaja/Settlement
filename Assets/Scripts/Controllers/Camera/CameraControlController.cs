using Models.Camera.Control;
using System;
using UnityEngine;

namespace Controllers.Camera;

public class CameraControlController : IDisposable
{
    private readonly CameraControl _cameraControl;
    
    public CameraControlController(CameraControl cameraControl)
    {
        _cameraControl = cameraControl;
    }
    
    public void UpdateMovement(Vector3 movementDirection, float zoomDirection, Vector2 rotationDirection, Vector3 forward, bool canStartZoom, bool canRotate, float deltaTime)
    {
        _cameraControl.UpdatePosition(movementDirection, zoomDirection, rotationDirection, forward, canStartZoom, canRotate, deltaTime);
    }

    public void StopZoom()
    {
        _cameraControl.StopZoom();
    }

    public void Dispose()
    {
        _cameraControl.Dispose();
    }
}
