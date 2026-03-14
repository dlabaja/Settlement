using Constants;
using Converters;
using Data.Init;
using Instances;
using JetBrains.Annotations;
using Services.Resources;
using System.Threading.Tasks;
using UnityEngine;

namespace Initializers;

public static class BootDataContainer
{
    [CanBeNull] public static BootData BootData { get; private set; }
    [CanBeNull] public static GameData GameData { get; private set; }

    public async static Task InitBootData()
    {
        BootData = new BootData
        {
            Settings = await SettingsConverter.FromJson(),
            VillagerNames = await VillagerNamesConverter.FromJson()
        };
    }
    
    public static void InitGameData(Terrain terrain)
    {
        var prefabs = PrefabsService.LoadAllPrefabs();
        GameData = new GameData
        {
            mousePositionAction = InputActionMaps.Mouse.FindAction(InputActionName.MousePosition),
            mousePositionDeltaAction = InputActionMaps.Mouse.FindAction(InputActionName.MouseDelta),
            terrain = terrain,
            worldObjectPrefabs = prefabs.worldObjectPrefabs,
            villagerPrefab = prefabs.villagerPrefab,
            materials = MaterialsService.LoadAllMaterials()
        };
    }
}
