using Interfaces;
using Models.Systems.Settings;
using Services;
using UnityEngine;

namespace Models.Camera.Control;

public class CameraRotation : ISettingsDependant
{
    private readonly SettingsValue<int> _rotationSpeed;

    public CameraRotation(SettingsService settingsService)
    {
        _rotationSpeed = settingsService.Value(s => s.CameraSettings.RotationSpeed);
    }
        
    public Vector3 RotationVectorDelta(Vector2 vector, float deltaTime)
    {
        var vector3 = new Vector3(-vector.y, vector.x, 0);
        return vector3 * (deltaTime * _rotationSpeed.Value);
    }

    public void Dispose()
    {
        _rotationSpeed.Dispose();
    }
}