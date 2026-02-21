using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Models.Systems.Inventory;

public class Inventory
{
    public int SlotCount { get; }
    private readonly InventorySlot[] _slots;

    public Inventory(int slotCount, int stackSize = InventorySlot.DefaultStackSize)
    {
        SlotCount = slotCount;
        _slots = ArrayUtils.CreateFilled(SlotCount, () => new InventorySlot(ItemType.None, stackSize));
    }

    public bool TryGetSlot(int index, out InventorySlot slot)
    {
        slot = null;
        if (ArrayUtils.OutOfBounds(_slots, index))
        {
            return false;
        }

        slot = _slots[index];
        return true;
    }

    public void Add(int count, ItemType itemType, out int overflow)
    {
        overflow = count;
        foreach (var slot in GetSlotsForType(itemType))
        {
            if (overflow == 0)
            {
                return;
            }

            if (!slot.HasItemType)
            {
                slot.ResetSlot(itemType);
            }

            slot.TryAddItems(overflow, out overflow);
        }
    }
    
    public void Remove(int count, ItemType itemType, out int underflow)
    {
        underflow = count;
        foreach (var slot in GetSlotsWithType(itemType))
        {
            if (underflow == 0)
            {
                return;
            }

            slot.TryRemoveItems(underflow, out underflow);
            
            if (slot.IsEmpty)
            {
                slot.ResetSlot(ItemType.None);
            }
        }
    }

    public void Reset()
    {
        foreach (var slot in _slots)
        {
            slot.ResetSlot(ItemType.None);
        }
    }

    public void Transfer(Inventory inventory, int count, ItemType itemType)
    {
        var itemCount = Math.Min(GetItemCount(itemType), count);
        Remove(itemCount, itemType, out _);
        inventory.Add(itemCount, itemType, out int overflow);
        Add(overflow, itemType, out _);
    }

    public int GetItemCount(ItemType type)
    {
        var slots = GetSlotsWithType(type);
        return slots.Sum(slot => slot.ItemCount);
    }

    public ItemType[] GetItemTypes()
    {
        return _slots.Select(slot => slot.ItemType).Distinct().ToArray();
    }

    // empty slots or slots with type of first arg
    private IEnumerable<InventorySlot> GetSlotsForType(ItemType type)
    {
        return _slots.Where(slot => slot.CanStoreItemType(type))
            .OrderBy(slot =>
                slot.IsEmpty ? 2 :
                slot.IsFull  ? 1 :
                0);;
    }

    private IEnumerable<InventorySlot> GetSlotsWithType(ItemType type)
    {
        return GetSlotsForType(type).Where(slot => slot.HasItemType);
    }
}
