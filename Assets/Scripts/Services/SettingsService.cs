using Constants;
using Data.Settings;
using Utils;

namespace Services;

public class SettingsService
{
    public Settings Settings { get; }
        
    public SettingsService(Settings settings)
    {
        Settings = settings;
    }

    public void SaveSettings()
    {
        IOUtils.SaveFile(Path.SettingsJson, JsonUtils.ModelToJson(Settings));
    }
}