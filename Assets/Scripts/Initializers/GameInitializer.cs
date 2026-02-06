using Data;
using Factories;
using Managers;
using Reflex.Core;
using Reflex.Enums;

namespace Initializers;

public class GameInitializer
{
    public void Init(ContainerBuilder builder, ClientData data)
    {
        RegisterManagers(builder, data);
        RegisterFactories(builder, data);
    }

    private void RegisterManagers(ContainerBuilder builder, ClientData data)
    {
        builder.RegisterValue(new SettingsManager(data.Settings));
        builder.RegisterValue(new VillagerConfigManager(data.VillagerNames));
    }

    private void RegisterFactories(ContainerBuilder builder, ClientData data)
    {
        builder.RegisterType(typeof(VillagerFactory), Lifetime.Singleton, Resolution.Lazy);
    }
}