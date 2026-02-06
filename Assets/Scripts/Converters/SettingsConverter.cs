using Constants;
using Data.Settings;
using Defaults;
using System.Threading.Tasks;
using Utils;

namespace Converters;

public static class SettingsConverter
{
    public async static Task<Settings> FromJson()
    {
        return await Converter.FromJson(Paths.SettingsJson, SettingsDefault.Settings);
    }
}
