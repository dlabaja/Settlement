using Services;
using System;

namespace Models.Systems.Settings;

public class SettingsValue<T> : IDisposable
{
    private readonly SettingsService _settingsService;
    // funkce bere na vstupu nastavení (data) a vrací T (datovej typ dat): fn(data) -> T
    private readonly Func<Data.Settings.Settings, T> _selector;
    public T Value { get; private set; }

    public SettingsValue(SettingsService settingsService, Func<Data.Settings.Settings, T> selector)
    {
        _settingsService = settingsService;
        _selector = selector;
        _settingsService.SettingsChanged += Update;
        Update();
    }

    private void Update() => Value = _selector(_settingsService.Settings);

    public void Dispose()
    {
        _settingsService.SettingsChanged -= Update;
    }
}
