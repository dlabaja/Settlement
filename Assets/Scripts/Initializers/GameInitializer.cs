using Data.Init;
using Factories;
using Reflex.Core;
using Services;
using Services.Controls;
using Services.GameObjects;
using Services.GameObjects.Villagers;
using Services.Resources;
using Services.Systems;

namespace Initializers;

public class GameInitializer : Initializer
{
    public GameInitializer(ContainerBuilder containerBuilder) : base(containerBuilder) {}

    public void Init(BootData bootData, GameData gameData)
    {
        RegisterServices(bootData, gameData);
        RegisterFactories();
    }

    private void RegisterServices(BootData bootData, GameData gameData)
    {
        RegisterService(new VillagerConfigService(bootData.VillagerNames));
        
        RegisterService(new MousePositionService(gameData.mousePositionAction, gameData.mousePositionDeltaAction));
        RegisterService(new PrefabsService(gameData.worldObjectPrefabs, gameData.villagerPrefab));
        RegisterService(new MaterialsService(gameData.materials));
        RegisterService(new TerrainService(gameData.terrain));
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
}