using Enums;
using Models.Systems.Inventory;
using Models.Villagers;
using System.Threading.Tasks;

namespace Models.WorldObjects.Buildings;

public class House : WorldObject
{
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.House;
    public override Inventory Inventory { get; } = new Inventory(0);
    
    public override Task VillagerTask(Villager villager)
    {
        throw new System.NotImplementedException();
    }
}
