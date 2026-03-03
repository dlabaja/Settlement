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
    private ContainerBuilder _builder;
    
    public void Init(ContainerBuilder builder, ClientData data, InitData initData)
    {
        _builder = builder;
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
        builder.RegisterValue(new GlobalInventoryService());
        builder.RegisterValue(new PathfindingService());
        builder.RegisterValue(new InteractionModeService());
    }

    private void RegisterFactories(ContainerBuilder builder)
    {
        RegisterFactory<VillagerFactory>();
        RegisterFactory<WorldObjectFactory>();
        RegisterFactory<VillagerTaskFactory>();
    }

    private void RegisterFactory<T>()
    {
        _builder.RegisterType(typeof(T), Lifetime.Singleton, Resolution.Lazy);
    }
}