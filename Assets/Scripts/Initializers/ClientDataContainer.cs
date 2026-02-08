using Converters;
using Data.Init;
using JetBrains.Annotations;
using System.Threading.Tasks;

namespace Initializers;

public static class ClientDataContainer
{
    [CanBeNull] public static ClientData ClientData { get; private set; }
    
    public async static Task Init()
    {
        ClientData = new ClientData
        {
            Settings = await SettingsConverter.FromJson(),
            VillagerNames = await VillagerNamesConverter.FromJson()
        };
    }
}
