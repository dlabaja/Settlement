using Data.Settings;

namespace Defaults;

public static class SettingsDefault
{
    public static readonly Settings Settings = new Settings
    {
        CameraSettings = new CameraSettings
        {
            ZoomSpeed = 40,
            RotationSpeed = 8,
            MoveSpeed = 10
        }
    };
}