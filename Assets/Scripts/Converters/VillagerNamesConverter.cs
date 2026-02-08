using Constants;
using Data.Villagers;
using Instances;
using System.Threading.Tasks;

namespace Converters;

public static class VillagerNamesConverter
{
    public async static Task<VillagerNames> FromJson()
    {
        return await Converter.FromJson(Path.VillagerNamesJson, VillagerNamesInstances.Default);
    }
}
