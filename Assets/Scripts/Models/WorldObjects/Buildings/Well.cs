using Enums;
using Models.Systems.Inventory;
using Models.Villagers;
using System.Threading.Tasks;

namespace Models.WorldObjects.Buildings;

public class Well : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Well;
    public override Inventory Inventory { get; } = new Inventory(0);
    
    public override Task VillagerTask(Villager villager)
    {
        villager.Stats.Water.Reset();
        return Task.CompletedTask;
    }
}
