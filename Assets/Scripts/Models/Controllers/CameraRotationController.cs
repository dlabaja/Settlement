using Managers;
using UnityEngine;

namespace Models.Controllers;

public class CameraRotationController
{
    private readonly SettingsManager _settingsManager;
    private int RotationSpeed => _settingsManager.Settings.CameraSettings.RotationSpeed;

    public CameraRotationController(SettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }
        
    public Vector3 VectorToRotationDelta(Vector2 vector, float deltaTime)
    {
        var vector3 = new Vector3(-vector.y, vector.x, 0);
        return vector3 * (deltaTime * RotationSpeed);
    }
}