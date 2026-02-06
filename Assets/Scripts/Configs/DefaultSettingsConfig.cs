using Models.Data.Settings;

namespace Configs
{
    public static class DefaultSettingsConfig
    {
        public static readonly Settings defaultSettings = new Settings
        {
            CameraSettings = new CameraSettings
            {
                ZoomSpeed = 40,
                RotationSpeed = 8,
                MoveSpeed = 10
            }
        };
    }
}
