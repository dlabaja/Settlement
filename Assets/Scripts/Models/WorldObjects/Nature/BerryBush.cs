using Enums;
using Models.Systems.Inventory;
using Models.Villagers;
using Services;
using System;
using System.Threading.Tasks;
using Utils;

namespace Models.WorldObjects.Nature;

// sealed = už z ní nemůžu dál dědit
public sealed class BerryBush : WorldObject
{
    private readonly TimerService _timerService;
    public override WorldObjectType WorldObjectType { get; } = WorldObjectType.BerryBush;
    public override Inventory Inventory { get; } = new Inventory(1);
    
    public BerryBush(TimerService timerService)
    {
        _timerService = timerService;
        _timerService.Register(RegrowBeries, 360);
        Inventory.Add(Inventory.StackSize, ItemType.Berries, out _);
    }
    
    public override Task VillagerTask(Villager villager)
    {
        Inventory.Transfer(villager.Inventory, RandomUtils.FromInterval(1, 3), ItemType.Berries);
        return Task.CompletedTask;
    }

    private void RegrowBeries()
    {
        Inventory.Fill(ItemType.Berries);
    }

    public override void Dispose()
    {
        _timerService.Remove(RegrowBeries);
    }
}
