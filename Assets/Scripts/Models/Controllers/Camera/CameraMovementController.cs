using Managers;
using UnityEngine;

namespace Models.Controllers.Camera;

public class CameraMovementController
{
    private readonly SettingsManager _settingsManager;
    private readonly Transform _transform;
    private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
    private float MoveSpeed => _settingsManager.Settings.CameraSettings.MoveSpeed;

    public CameraMovementController(Transform transform, SettingsManager settingsManager)
    {
        _transform = transform;
        _settingsManager = settingsManager;
    }

    public Vector3 MovedVectorDelta(Vector3 vector, float deltaTime)
    {
        vector.Scale(_planeLockVector);
        return vector * (MoveSpeed * deltaTime);
    }

    public Vector3 Forward()
    {
        return _transform.forward;
    }

    public Vector3 Backward()
    {
        return -_transform.forward;
    }

    public Vector3 Right()
    {
        return _transform.right;
    }

    public Vector3 Left()
    {
        return -_transform.right;
    }
}