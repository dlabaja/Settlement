using Enums;
using Models.Systems.Inventory;
using Models.Villagers;
using System.Threading.Tasks;
using Utils;

namespace Models.WorldObjects.Nature;

public sealed class Tree : WorldObject
{
    public int HP { get; private set; } = 100;
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.Tree;
    public override Inventory Inventory { get; } = new Inventory(1);

    public Tree()
    {
        Inventory.Add(RandomUtils.FromInterval(3, 10), ItemType.Wood, out _);
    }
    
    public override Task VillagerTask(Villager villager)
    {
        HP -= RandomUtils.FromInterval(5, 30);
        if (HP <= 0)
        {
            Inventory.Transfer(villager.Inventory, 100, ItemType.Wood);
            InvokeDestroy(this);
        }

        return Task.CompletedTask;
    }
}
