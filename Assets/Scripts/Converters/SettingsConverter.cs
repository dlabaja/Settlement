using Constants;
using Data.Settings;
using Instances;
using System.Threading.Tasks;
using Utils;

namespace Converters;

public static class SettingsConverter
{
    public async static Task<Settings> FromJson()
    {
        return await Converter.FromJson(Path.SettingsJson, SettingsInstances.Default);
    }
}
