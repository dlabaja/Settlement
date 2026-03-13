using Data.Init;
using Factories;
using Reflex.Core;
using Reflex.Enums;
using Services;
using Services.Controls;
using Services.GameObjects;
using Services.GameObjects.Villagers;
using Services.Resources;
using Services.Systems;
using Resolution = Reflex.Enums.Resolution;

namespace Initializers;

public class GameInitializer
{
    private ContainerBuilder _builder;
    
    public void Init(ContainerBuilder builder, ClientData data, InitData initData)
    {
        _builder = builder;
        RegisterServices(data, initData);
        RegisterFactories();
    }

    private void RegisterServices(ClientData data, InitData initData)
    {
        RegisterService(new SettingsService(data.Settings));
        RegisterService(new VillagerConfigService(data.VillagerNames));
        
        RegisterService(new MousePositionService(initData.mousePositionAction, initData.mousePositionDeltaAction));
        RegisterService(new PrefabsService(initData.worldObjectPrefabs, initData.villagerPrefab));
        RegisterService(new MaterialsService(initData.materials));
        RegisterService(new TerrainService(initData.terrain));
        RegisterService(new CameraRaycastService());
        
        RegisterService(new WorldObjectsService());
        RegisterService(new VillagerService());
        RegisterService(new GameTimeService());
        RegisterService(new GlobalInventoryService());
        RegisterService(new PathfindingService());
        RegisterService(new InteractionModeService());
    }

    private void RegisterFactories()
    {
        RegisterFactory<VillagerFactory>();
        RegisterFactory<WorldObjectFactory>();
        RegisterFactory<VillagerTaskFactory>();
        RegisterFactory<InteractionPointFactory>();
    }

    private void RegisterService(object value)
    {
        _builder.RegisterValue(value);
    }

    private void RegisterFactory<T>()
    {
        _builder.RegisterType(typeof(T), Lifetime.Singleton, Resolution.Lazy);
    }
}