using Enums;
using Models.Systems.Inventory;
using NUnit.Framework;

namespace Tests.Models.Systems.Inventory;

public class InventorySlotTest
{
    [Test]
    public void AddItemsToEmptySlot()
    {
        var slot = new InventorySlot(ItemType.None);
        var success = slot.AddItems(5, out var overflow);
        Assert.False(success);
        Assert.AreEqual(0, slot.ItemCount);
        Assert.AreEqual(0, overflow);
    }
    
    [Test]
    public void AddItems()
    {
        var slot = new InventorySlot(ItemType.Wood);
        var success = slot.AddItems(5, out var overflow);
        Assert.True(success);
        Assert.AreEqual(5, slot.ItemCount);
        Assert.AreEqual(0, overflow);
    }
    
    [Test]
    public void AddItemsOverflow()
    {
        var slot = new InventorySlot(ItemType.Wood);
        var success = slot.AddItems(400, out var overflow);
        Assert.True(success);
        Assert.AreEqual(InventorySlot.StackSize, slot.ItemCount);
        Assert.AreEqual(300, overflow);
    }
    
    [Test]
    public void RemoveItemsToEmptySlot()
    {
        var slot = new InventorySlot(ItemType.None);
        var success = slot.RemoveItems(5, out var underflow);
        Assert.False(success);
        Assert.AreEqual(0, slot.ItemCount);
        Assert.AreEqual(0, underflow);
    }
    
    [Test]
    public void RemoveItems()
    {
        var slot = new InventorySlot(ItemType.Wood);
        slot.AddItems(9, out _);
        var success = slot.RemoveItems(5, out var underflow);
        Assert.True(success);
        Assert.AreEqual(4, slot.ItemCount);
        Assert.AreEqual(0, underflow);
    }
    
    [Test]
    public void RemoveItemsUnderflow()
    {
        var slot = new InventorySlot(ItemType.Wood);
        var success = slot.RemoveItems(400, out var underflow);
        Assert.True(success);
        Assert.AreEqual(0, slot.ItemCount);
        Assert.AreEqual(400, underflow);
    }
}
