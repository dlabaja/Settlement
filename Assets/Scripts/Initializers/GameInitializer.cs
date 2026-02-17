using Data.Init;
using Factories;
using Reflex.Core;
using Reflex.Enums;
using Services;
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
        builder.RegisterValue(new SettingsService(data.Settings));
        builder.RegisterValue(new VillagerConfigManager(data.VillagerNames));
        builder.RegisterValue(new MousePositionService(initData.mousePositionAction, initData.mousePositionDeltaAction));
        
        builder.RegisterValue(new MaterialsService(MaterialsService.LoadAllMaterials()));
        
        builder.RegisterValue(new PlaceService());
    }

    private void RegisterFactories(ContainerBuilder builder)
    {
        builder.RegisterType(typeof(VillagerFactory), Lifetime.Singleton, Resolution.Lazy);
        builder.RegisterType(typeof(CustomObjectFactory), Lifetime.Singleton, Resolution.Lazy);
    }
}