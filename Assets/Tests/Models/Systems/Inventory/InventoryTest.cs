using Enums;
using NUnit.Framework;

namespace Tests.Models.Systems.Inventory;

public class InventoryTest
{
    private global::Models.Systems.Inventory.Inventory GetInventory(int slotCount)
    {
        return new global::Models.Systems.Inventory.Inventory(slotCount);
    }

    [Test]
    public void Constructor()
    {
        var inventory = GetInventory(4);
        Assert.AreEqual(inventory.SlotCount, 4);
        for (int i = 0; i < 4; i++)
        {
            inventory.TryGetSlot(i, out var slot);
            Assert.Zero(slot.ItemCount);
            Assert.AreEqual(slot.ItemType, ItemType.None);
        }
    }
    
    [Test]
    public void TryGetSlot()
    {
        var inventory = GetInventory(2);
        Assert.True(inventory.TryGetSlot(0, out _));
        Assert.True(inventory.TryGetSlot(1, out _));
        Assert.False(inventory.TryGetSlot(2, out var slot));
        Assert.Null(slot);
    }

    [Test]
    public void AddOverflow()
    {
        var inventory = GetInventory(3);
        inventory.Add(450, ItemType.Stone, out var overflow);
        Assert.AreEqual(overflow, 150);
        for (int i = 0; i < 3; i++)
        {
            inventory.TryGetSlot(i, out var slot);
            Assert.True(slot.IsFull);
            Assert.AreEqual(slot.ItemType, ItemType.Stone);
        }
    }

    [Test]
    public void Add()
    {
        var inventory = GetInventory(3);
        inventory.Add(150, ItemType.Berries, out var overflow);
        Assert.Zero(overflow);
        
        inventory.TryGetSlot(0, out var slot1);
        Assert.AreEqual(slot1.ItemCount, 100);
        Assert.AreEqual(slot1.ItemType, ItemType.Berries);
        
        inventory.TryGetSlot(1, out var slot2);
        Assert.AreEqual(slot2.ItemCount, 50);
        Assert.AreEqual(slot2.ItemType, ItemType.Berries);
        
        inventory.TryGetSlot(2, out var slot3);
        Assert.AreEqual(slot3.ItemCount, 0);
        Assert.AreEqual(slot3.ItemType, ItemType.None);
    }
    
    [Test]
    public void RemoveUnderflow()
    {
        var inventory = GetInventory(3);
        inventory.Add(150, ItemType.Stone, out _);
        inventory.Remove(450, ItemType.Stone, out var underflow);
        Assert.AreEqual(underflow, 300);
        for (int i = 0; i < 3; i++)
        {
            inventory.TryGetSlot(i, out var slot);
            Assert.True(slot.IsEmpty);
            Assert.AreEqual(slot.ItemType, ItemType.None);
        }
    }

    [Test]
    public void Remove()
    {
        var inventory = GetInventory(3);
        inventory.Add(150, ItemType.Berries, out _);
        inventory.Remove(60, ItemType.Berries, out var underflow);
        inventory.Remove(100, ItemType.Stone, out var underflow2);
        
        Assert.AreEqual(underflow, 0);
        Assert.AreEqual(underflow2, 100);
        
        inventory.TryGetSlot(0, out var slot1);
        Assert.AreEqual(slot1.ItemCount, 90);
        Assert.AreEqual(slot1.ItemType, ItemType.Berries);
        
        inventory.TryGetSlot(1, out var slot2);
        Assert.AreEqual(slot2.ItemCount, 0);
        Assert.AreEqual(slot2.ItemType, ItemType.None);
        
        inventory.TryGetSlot(2, out var slot3);
        Assert.AreEqual(slot3.ItemCount, 0);
        Assert.AreEqual(slot3.ItemType, ItemType.None);
    }
}
