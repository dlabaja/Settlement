using Services;
using UnityEngine;

namespace Models.Camera;

public class CameraRotation
{
    private readonly SettingsService _settingsService;
    private int RotationSpeed => _settingsService.Settings.CameraSettings.RotationSpeed;

    public CameraRotation(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }
        
    public Vector3 VectorToRotationDelta(Vector2 vector, float deltaTime)
    {
        var vector3 = new Vector3(-vector.y, vector.x, 0);
        return vector3 * (deltaTime * RotationSpeed);
    }
}