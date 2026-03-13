using Services;
using System;
using UnityEngine;

namespace Models.Camera.Control;

public delegate void CameraControlPositionChanged(Vector3 movementDelta, Vector3 zoomDelta, Vector3 rotationDelta);

public class CameraControl : IDisposable
{
    private readonly CameraMovement _cameraMovement;
    private readonly CameraRotation _cameraRotation;
    private readonly CameraZoom _cameraZoom;
    public event CameraControlPositionChanged PositionUpdated;
    
    public CameraControl(SettingsService settingsService)
    {
        _cameraMovement = new CameraMovement(settingsService);
        _cameraRotation = new CameraRotation(settingsService);
        _cameraZoom = new CameraZoom(settingsService);
    }

    public void UpdatePosition(Vector3 movementDirection, float zoomDirection, Vector2 rotationDirection, Vector3 forward, bool canStartZoom, bool canRotate, float deltaTime)
    {
        var movement = MovementDelta(movementDirection, deltaTime);
        var zoom = ZoomDelta(canStartZoom, zoomDirection, forward, deltaTime);
        var rotation = RotationDelta(canRotate, rotationDirection, deltaTime);
        PositionUpdated?.Invoke(movement, zoom, rotation);
    }

    public void StopZoom()
    {
        _cameraZoom.StopZoom();
    }
    
    private Vector3 MovementDelta(Vector3 movementDirection, float deltaTime)
    {
        return _cameraMovement.MovementVectorDelta(movementDirection, deltaTime);
    }

    private Vector3 ZoomDelta(bool canStartZoom, float direction, Vector3 forward, float deltaTime)
    {
        if (canStartZoom)
        {
            _cameraZoom.StartZoom(direction);
        }
            
        return _cameraZoom.ZoomVectorDelta(forward, deltaTime);
    }

    private Vector3 RotationDelta(bool canRotate, Vector2 rotationDirection, float deltaTime)
    {
        return canRotate ? _cameraRotation.RotationVectorDelta(rotationDirection, deltaTime) : Vector3.zero;

    }

    public void Dispose()
    {
        _cameraMovement.Dispose();
        _cameraRotation.Dispose();
        _cameraZoom.Dispose();
    }
}
