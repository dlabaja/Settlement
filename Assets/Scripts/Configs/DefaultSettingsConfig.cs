using Models.Data.Settings;

namespace Configs
{
    public static class DefaultSettingsConfig
    {
        public static readonly Settings defaultSettings = new Settings
        {
            CameraSettings = new CameraSettings
            {
                ZoomSpeed = 0,
                RotationSpeed = 0,
                MoveSpeed = 0
            }
        };
    }
}
