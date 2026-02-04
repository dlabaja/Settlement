using Models.Data.Settings;

namespace Managers
{
    public class SettingsManager
    {
        public SettingsManager(SettingsModel settings)
        {
            
        }

        public static SettingsModel LoadSettings()
        {
            return new SettingsModel();
        }

        public static void SaveSettings()
        {
            
        }
    }
}
