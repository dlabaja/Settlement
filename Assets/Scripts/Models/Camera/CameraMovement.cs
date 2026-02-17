using Managers;
using UnityEngine;

namespace Models.Camera;

public class CameraMovement
{
    private readonly SettingsManager _settingsManager;
    private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
    private float MoveSpeed => _settingsManager.Settings.CameraSettings.MoveSpeed;

    public CameraMovement(SettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }

    public Vector3 MovedVectorDelta(Vector3 vector, float deltaTime)
    {
        vector.Scale(_planeLockVector);
        return vector * (MoveSpeed * deltaTime);
    }
}