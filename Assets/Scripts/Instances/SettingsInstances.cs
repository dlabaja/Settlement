using Data.Settings;

namespace Instances;

public static class SettingsInstances
{
    public static readonly Settings Default = new Settings
    {
        CameraSettings = new CameraSettings
        {
            ZoomSpeed = 40,
            RotationSpeed = 8,
            MoveSpeed = 10
        }
    };
}