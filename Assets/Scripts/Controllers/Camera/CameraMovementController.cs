using Models.Camera;
using Services;
using UnityEngine;

namespace Controllers.Camera;

public class CameraMovementController
{
    private readonly CameraMovement _cameraMovement;
    private readonly CameraZoom _cameraZoom;
    private readonly CameraRotation _cameraRotation;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    
    public CameraMovementController(Rigidbody rigidbody, Transform transform, SettingsService settingsService)
    {
        _cameraMovement = new CameraMovement(settingsService);
        _cameraZoom = new CameraZoom(settingsService);
        _cameraRotation = new CameraRotation(settingsService);
        _rigidbody = rigidbody;
        _transform = transform;
    }
    
    public void UpdateMovement(Vector3 movementDirection, bool canStartZoom, float zoomDirection, bool canRotate, Vector2 rotationDelta)
    {
        var movementVector = MovementDelta(movementDirection);
        var zoomedVector = ZoomDelta(canStartZoom, zoomDirection);
        var rotation = RotationDelta(canRotate, rotationDelta);
        ApplyMovement(movementVector, zoomedVector, rotation);
    }

    public void StopZoom()
    {
        _cameraZoom.StopZoom();
    }

    private void ApplyMovement(Vector3 movementDelta, Vector3 zoomDelta, Vector3 rotationDelta)
    {
        _rigidbody.Move(
            _transform.position + movementDelta + zoomDelta,
            Quaternion.Euler(_transform.eulerAngles + rotationDelta));
    }
    
    private Vector3 MovementDelta(Vector3 movementDirection)
    {
        return _cameraMovement.MovedVectorDelta(movementDirection, Time.deltaTime);
    }

    private Vector3 ZoomDelta(bool canStartZoom, float direction)
    {
        if (canStartZoom)
        {
            _cameraZoom.StartZoom(direction);
        }
            
        return _cameraZoom.ZoomedVectorDelta(_transform.forward, Time.deltaTime);
    }

    private Vector3 RotationDelta(bool rotationKeyPressed, Vector3 mouseDelta)
    {
        return rotationKeyPressed 
            ? _cameraRotation.VectorToRotationDelta(mouseDelta, Time.deltaTime) 
            : Vector3.zero;
    }
}
