using Data.Init;
using Factories;
using Reflex.Core;
using Reflex.Enums;
using Services;
using Services.GameObjects;
using Services.Resources;
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
        builder.RegisterValue(new VillagerConfigService(data.VillagerNames));
        builder.RegisterValue(new MousePositionService(initData.mousePositionAction, initData.mousePositionDeltaAction));
        builder.RegisterValue(new PrefabsService(initData.worldObjectPrefabs, initData.villagerPrefab));
        builder.RegisterValue(new MaterialsService(initData.materials));
        builder.RegisterValue(new TerrainService(initData.terrain));
        
        builder.RegisterValue(new WorldObjectsService());
        builder.RegisterValue(new VillagerService());
        builder.RegisterValue(new GameTimeService());
        builder.RegisterValue(new GlobalInventory());
        builder.RegisterValue(new InteractionModeService());
    }

    private void RegisterFactories(ContainerBuilder builder)
    {
        builder.RegisterType(typeof(VillagerFactory), Lifetime.Singleton, Resolution.Lazy);
        builder.RegisterType(typeof(WorldObjectFactory), Lifetime.Singleton, Resolution.Lazy);
    }
}