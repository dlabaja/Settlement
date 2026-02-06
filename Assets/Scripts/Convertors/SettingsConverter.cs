using Constants;
using Data.Settings;
using Defaults;
using System.Threading.Tasks;
using Utils;

namespace Convertors;

public static class SettingsConverter
{
    public async static Task<Settings> FromJson()
    {
        try
        {
            var data = await JsonUtils.JsonToModel<Settings>(Paths.SettingsJson);
            return data;
        }
        catch
        {
            return SettingsDefault.Settings;
        }
    }
}
