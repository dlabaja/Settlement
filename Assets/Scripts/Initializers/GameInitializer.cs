using Data.Init;
using Factories;
using Managers;
using Reflex.Core;
using Reflex.Enums;
using Resolution = Reflex.Enums.Resolution;

namespace Initializers;

public class GameInitializer
{
    public void Init(ContainerBuilder builder, ClientData data, InitData initData)
    {
        RegisterManagers(builder, data, initData);
        RegisterFactories(builder);
    }

    private void RegisterManagers(ContainerBuilder builder, ClientData data, InitData initData)
    {
        builder.RegisterValue(new SettingsManager(data.Settings));
        builder.RegisterValue(new VillagerConfigManager(data.VillagerNames));
        builder.RegisterValue(new MaterialsManager(MaterialsManager.LoadAllMaterials()));
        builder.RegisterValue(new MousePositionManager(initData.mousePositionAction, initData.mousePositionDeltaAction));
    }

    private void RegisterFactories(ContainerBuilder builder)
    {
        builder.RegisterType(typeof(VillagerFactory), Lifetime.Singleton, Resolution.Lazy);
    }
}