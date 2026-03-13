using Interfaces;
using Models.Systems.Settings;
using Services;
using UnityEngine;

namespace Models.Camera.Control;

public class CameraZoom : ISettingsDependant
{
    private int _remainingTicks = maxRemainingTicks;
    private float _direction;
    private const int maxRemainingTicks = 20;
    private readonly SettingsValue<int> _zoomSpeed;

    public CameraZoom(SettingsService settingsService)
    {
        _zoomSpeed = settingsService.Value(s => s.CameraSettings.ZoomSpeed);
    }
        
    public void StartZoom(float direction)
    {
        _direction = direction;
        _remainingTicks = maxRemainingTicks;
    }

    public Vector3 ZoomVectorDelta(Vector3 forward, float deltaTime)
    {
        if (ZoomEnded)
        {
            return Vector3.zero;
        }
        _remainingTicks--;
        return forward * (_direction * _zoomSpeed.Value * deltaTime);
    }

    public bool ZoomEnded
    {
        get => _remainingTicks == 0;
    }

    public void StopZoom()
    {
        _remainingTicks = 0;
    }

    public void Dispose()
    {
        _zoomSpeed.Dispose();
    }
}