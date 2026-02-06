using Constants;
using Data.Settings;
using Utils;

namespace Managers;

public class SettingsManager
{
    public Settings Settings { get; }
        
    public SettingsManager(Settings settings)
    {
        Settings = settings;
    }

    public void SaveSettings()
    {
        IOUtils.SaveFile(Paths.SettingsJson, JsonUtils.ModelToJson(Settings));
    }
}