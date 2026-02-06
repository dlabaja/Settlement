using Configs;
using Constants;
using Models.Data.Settings;
using System.Threading.Tasks;
using Utils;

namespace Managers;

public class SettingsManager
{
    public Settings Settings { get; }
        
    public SettingsManager(Settings settings)
    {
        Settings = settings;
    }

    public async static Task<Settings> GetSettings()
    {
        try
        {
            var data = await JsonUtils.JsonToModel<Settings>(Paths.SettingsJson);
            return data;
        }
        catch
        {
            return DefaultSettingsConfig.defaultSettings;
        }
    }

    public void SaveSettings()
    {
        IOUtils.SaveFile(Paths.SettingsJson, JsonUtils.ModelToJson(Settings));
    }
}