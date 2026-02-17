using Services;
using UnityEngine;

namespace Models.Camera;

public class CameraMovement
{
    private readonly SettingsService _settingsService;
    private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
    private float MoveSpeed => _settingsService.Settings.CameraSettings.MoveSpeed;

    public CameraMovement(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public Vector3 MovedVectorDelta(Vector3 vector, float deltaTime)
    {
        vector.Scale(_planeLockVector);
        return vector * (MoveSpeed * deltaTime);
    }
}