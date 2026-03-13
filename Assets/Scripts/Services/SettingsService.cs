using Constants;
using Data.Settings;
using Models.Systems.Settings;
using System;
using Utils;

namespace Services;

public class SettingsService
{
    public Settings Settings { get; }
    public event Action SettingsChanged;
        
    public SettingsService(Settings settings)
    {
        Settings = settings;
    }

    public SettingsValue<T> Value<T>(Func<Settings, T> setting)
    {
        return new SettingsValue<T>(this, setting);
    }

    public void SaveSettings()
    {
        SettingsChanged?.Invoke();
        IOUtils.SaveFile(Path.SettingsJson, JsonUtils.ModelToJson(Settings));
    }
}