using Enums;
using Services.GameObjects;
using Services.GameObjects.Villagers;
using Services.Resources;
using Services.Systems;

namespace Controllers.Systems;

public class GameStartController
{
    public void Init(PrefabsService prefabsService)
    {
        //prefabsService.SpawnVillager(new Vector3(0, 0, 0));
        //prefabsService.SpawnVillager(new Vector3(0, 1, 0));
    }
    
    public void Start(WorldObjectsService worldObjectsService,
        VillagerService villagerService, GameTimeService gameTimeService)
    {
        var spawn = worldObjectsService.GetFirstWorldObject(WorldObjectType.Spawn);
        foreach (var villager in villagerService.Villagers)
        {
            villager.Places.Workplace = spawn;
            villager.Stats.Water.Decrease(60);
        }
        gameTimeService.Play(1);
    }
}
