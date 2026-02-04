using Models.Data.Settings;

namespace Managers
{
    public class SettingsManager
    {
        public SettingsManager(Settings settings)
        {
            
        }

        public static Settings LoadSettings()
        {
            return new Settings();
        }

        public static void SaveSettings()
        {
            
        }
    }
}
