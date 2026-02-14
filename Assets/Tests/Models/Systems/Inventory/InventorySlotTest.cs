using Enums;
using Models.Systems.Inventory;
using NUnit.Framework;

namespace Tests.Models.Systems.Inventory;

public class InventorySlotTest
{
    [Test]
    public void Constructor()
    {
        var slot = new InventorySlot(ItemType.Stone);
        Assert.Zero(slot.ItemCount);
        Assert.AreEqual(slot.ItemType, ItemType.Stone);
    }
    
    [Test]
    public void AddItemsToEmptySlot()
    {
        var slot = new InventorySlot(ItemType.None);
        var success = slot.TryAddItems(5, out var overflow);
        Assert.False(success);
        Assert.Zero(slot.ItemCount);
        Assert.Zero(overflow);
    }
    
    [Test]
    public void AddItems()
    {
        var slot = new InventorySlot(ItemType.Wood);
        var success = slot.TryAddItems(5, out var overflow);
        Assert.True(success);
        Assert.AreEqual(slot.ItemCount, 5);
        Assert.Zero(overflow);
    }
    
    [Test]
    public void AddItemsOverflow()
    {
        var slot = new InventorySlot(ItemType.Wood);
        var success = slot.TryAddItems(400, out var overflow);
        Assert.True(success);
        Assert.AreEqual(InventorySlot.StackSize, slot.ItemCount);
        Assert.AreEqual(300, overflow);
    }
    
    [Test]
    public void RemoveItemsToEmptySlot()
    {
        var slot = new InventorySlot(ItemType.None);
        var success = slot.TryRemoveItems(5, out var underflow);
        Assert.False(success);
        Assert.Zero(slot.ItemCount);
        Assert.Zero(underflow);
    }
    
    [Test]
    public void RemoveItems()
    {
        var slot = new InventorySlot(ItemType.Wood);
        slot.TryAddItems(9, out _);
        var success = slot.TryRemoveItems(5, out var underflow);
        Assert.True(success);
        Assert.AreEqual(4, slot.ItemCount);
        Assert.Zero(underflow);
    }
    
    [Test]
    public void RemoveItemsUnderflow()
    {
        var slot = new InventorySlot(ItemType.Wood);
        var success = slot.TryRemoveItems(400, out var underflow);
        Assert.True(success);
        Assert.Zero(slot.ItemCount);
        Assert.AreEqual(400, underflow);
    }

    [Test]
    public void ResetSlot()
    {
        var slot = new InventorySlot(ItemType.Wood);
        slot.TryAddItems(5, out _);
        Assert.AreEqual(5, slot.ItemCount);
        slot.ResetSlot(ItemType.Berries);
        Assert.Zero(slot.ItemCount);
        Assert.AreEqual(slot.ItemType, ItemType.Berries);
    }

    [Test]
    public void EmptySlot()
    {
        var slot = new InventorySlot(ItemType.None);
        var success = slot.EmptySlot(out var removedItems);
        Assert.False(success);
        Assert.Zero(removedItems);
    }

    [Test]
    public void CanStoreItemType()
    {
        var slot = new InventorySlot(ItemType.None);
        Assert.True(slot.CanStoreItemType(ItemType.Stone));
        
        slot.ResetSlot(ItemType.Berries);
        Assert.True(slot.CanStoreItemType(ItemType.Berries));
        Assert.False(slot.CanStoreItemType(ItemType.Stone));
    }
}
