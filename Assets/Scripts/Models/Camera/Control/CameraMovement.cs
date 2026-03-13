using Interfaces;
using Models.Systems.Settings;
using Services;
using UnityEngine;

namespace Models.Camera.Control;

public class CameraMovement : ISettingsDependant
{
    private readonly Vector3 _planeLockVector = new Vector3(1, 0, 1);
    private readonly SettingsValue<int> _moveSpeed;

    public CameraMovement(SettingsService settingsService)
    {
        _moveSpeed = settingsService.Value(s => s.CameraSettings.MoveSpeed);
    }

    public Vector3 MovementVectorDelta(Vector3 vector, float deltaTime)
    {
        vector.Scale(_planeLockVector);
        return vector * (_moveSpeed.Value * deltaTime);
    }

    public void Dispose()
    {
        _moveSpeed.Dispose();
    }
}