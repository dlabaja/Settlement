using Constants;
using Data.Villagers;
using Defaults;
using System.Threading.Tasks;

namespace Converters;

public static class VillagerNamesConverter
{
    public async static Task<VillagerNames> FromJson()
    {
        return await Converter.FromJson(Paths.VillagerNamesJson, VillagerNamesDefault.VillagerNames);
    }
}
