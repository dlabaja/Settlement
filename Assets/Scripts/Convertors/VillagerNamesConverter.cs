using Constants;
using Data.Villagers;
using Defaults;
using System.Threading.Tasks;
using Utils;

namespace Convertors;

public static class VillagerNamesConverter
{
    public async static Task<VillagerNames> FromJson()
    {
        try
        {
            var data = await JsonUtils.JsonToModel<VillagerNames>(Paths.VillagerNamesJson);
            return data;
        }
        catch
        {
            return VillagerNamesDefault.VillagerNames;
        }
    }
}
