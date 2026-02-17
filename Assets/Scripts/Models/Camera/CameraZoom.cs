using Services;
using UnityEngine;

namespace Models.Camera;

public class CameraZoom
{
    private readonly SettingsService _settingsService;
    private int _remainingTicks = maxRemainingTicks;
    private float _direction;
    private const int maxRemainingTicks = 20;
    private float ZoomSpeed => _settingsService.Settings.CameraSettings.ZoomSpeed;

    public CameraZoom(SettingsService settingsService)
    {
        _settingsService = settingsService;
    }
        
    public void StartZoom(float direction)
    {
        _direction = direction;
        _remainingTicks = maxRemainingTicks;
    }

    public Vector3 ZoomedVectorDelta(Vector3 forward, float deltaTime)
    {
        if (ZoomEnded)
        {
            return Vector3.zero;
        }
        _remainingTicks--;
        return forward * (_direction * ZoomSpeed * deltaTime);
    }

    public bool ZoomEnded
    {
        get => _remainingTicks == 0;
    }

    public void StopZoom()
    {
        _remainingTicks = 0;
    }
}