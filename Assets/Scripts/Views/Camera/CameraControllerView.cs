using Models.Camera;
using UnityEngine;

namespace Views.Camera;

public class CameraControllerView
{
    private readonly CameraMovement _cameraMovement;
    private readonly CameraZoom _cameraZoom;
    private readonly CameraRotation _cameraRotation;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;

    public CameraControllerView(GameObject camera, CameraMovement cameraMovement, 
        CameraZoom cameraZoom, CameraRotation cameraRotation)
    {
        _cameraMovement = cameraMovement;
        _cameraZoom = cameraZoom;
        _cameraRotation = cameraRotation;
        _rigidbody = camera.GetComponent<Rigidbody>();
        _transform = camera.transform;
    }
    
    public void Process(Vector3 movementDelta, Vector3 zoomDelta, Vector3 rotationDelta)
    {
        _rigidbody.Move(
            _transform.position + movementDelta + zoomDelta,
            Quaternion.Euler(_transform.eulerAngles + rotationDelta));
    }
    
    public Vector3 MovementDelta(Vector3 movementDirection)
    {
        return _cameraMovement.MovedVectorDelta(movementDirection, Time.deltaTime);
    }

    public Vector3 ZoomDelta(bool zoomPerformed, float direction)
    {
        if (zoomPerformed)
        {
            _cameraZoom.StartZoom(direction);
        }
            
        return _cameraZoom.ZoomedVectorDelta(_transform.forward, Time.deltaTime);
    }

    public Vector3 RotationDelta(bool rotationKeyPressed, Vector3 mouseDelta)
    {
        return rotationKeyPressed 
            ? _cameraRotation.VectorToRotationDelta(mouseDelta, Time.deltaTime) 
            : Vector3.zero;
    }
}
