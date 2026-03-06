using Models.Villagers;
using Models.WorldObjects;
using Services.Resources;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers.GameObjects;

public class InteractionPointController
{
    public InteractionPoint GetGrounded(Vector3 pos, TerrainService terrainService)
    {
        return new InteractionPoint(terrainService.GroundVector3(pos));
    }

    public async Task OnVillagerCollision(Villager villager, WorldObject destination)
    {
        if (villager.Tasks.CurrentRunningTask == null)
        {
            return;
        }

        if (villager.Tasks.CurrentRunningTask.Destination != destination)
        {
            return;
        }
        
        await villager.Tasks.RunCurrentTask();
    }
}
