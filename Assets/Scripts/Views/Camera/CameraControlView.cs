using Models.Camera.Control;
using System;
using UnityEngine;

namespace Views.Camera;

public class CameraControlView : IDisposable
{
    private readonly Rigidbody _rigidbody;
    private readonly CameraControl _cameraControl;
    private readonly Transform _transform;
    
    public CameraControlView(CameraControl cameraControl, Rigidbody rigidbody, Transform transform)
    {
        _cameraControl = cameraControl;
        _rigidbody = rigidbody;
        _transform = transform;
        _cameraControl.PositionUpdated += CameraControlOnPositionUpdated;
    }

    private void CameraControlOnPositionUpdated(Vector3 movementDelta, Vector3 zoomDelta, Vector3 rotationDelta)
    {
        _rigidbody.Move(
            _transform.position + movementDelta + zoomDelta,
            Quaternion.Euler(_transform.eulerAngles + rotationDelta));
    }

    public void Dispose()
    {
        _cameraControl.PositionUpdated -= CameraControlOnPositionUpdated;
    }
}
