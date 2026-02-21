using Constants;
using Data.Init;
using Factories;
using Reflex.Core;
using Reflex.Enums;
using Services;
using System.Linq;
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
        
        var prefabs = PrefabsService.LoadAllPrefabs();
        var villagerPrefab = prefabs.First(obj => obj.name == PrefabName.Villager);
        var worldObjectPrefabs = prefabs.Except(new[] {villagerPrefab}).ToArray();
        builder.RegisterValue(new PrefabsService(worldObjectPrefabs, villagerPrefab));
        builder.RegisterValue(new MaterialsService(MaterialsService.LoadAllMaterials()));
        
        builder.RegisterValue(new WorldObjectsService());
        builder.RegisterValue(new GameTimeService());
        builder.RegisterValue(new GlobalInventory());
    }

    private void RegisterFactories(ContainerBuilder builder)
    {
        builder.RegisterType(typeof(VillagerFactory), Lifetime.Singleton, Resolution.Lazy);
        builder.RegisterType(typeof(WorldObjectFactory), Lifetime.Singleton, Resolution.Lazy);
    }
}