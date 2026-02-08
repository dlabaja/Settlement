using Models.Controllers.Camera;
using UnityEngine;

namespace Views.Camera;

public class CameraControllerView
{
    private readonly CameraMovementController _cameraMovementController;
    private readonly CameraZoomController _cameraZoomController;
    private readonly CameraRotationController _cameraRotationController;
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;

    public CameraControllerView(GameObject camera, CameraMovementController cameraMovementController, 
        CameraZoomController cameraZoomController, CameraRotationController cameraRotationController)
    {
        _cameraMovementController = cameraMovementController;
        _cameraZoomController = cameraZoomController;
        _cameraRotationController = cameraRotationController;
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
        return _cameraMovementController.MovedVectorDelta(movementDirection, Time.deltaTime);
    }

    public Vector3 ZoomDelta(bool zoomPerformed, float direction)
    {
        if (zoomPerformed)
        {
            _cameraZoomController.StartZoom(direction);
        }
            
        return _cameraZoomController.ZoomedVectorDelta(_transform.forward, Time.deltaTime);
    }

    public Vector3 RotationDelta(bool rotationKeyPressed, Vector3 mouseDelta)
    {
        return rotationKeyPressed 
            ? _cameraRotationController.VectorToRotationDelta(mouseDelta, Time.deltaTime) 
            : Vector3.zero;
    }
}
