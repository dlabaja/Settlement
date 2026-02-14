using Enums;

namespace Models.Systems.Inventory;

public class InventorySlot
{
    public const int StackSize = 100;
    public ItemType ItemType { get; private set; }
    public int ItemCount { get; private set; }

    public InventorySlot(ItemType itemType)
    {
        ItemType = itemType;
        ItemCount = 0;
    }

    public bool AddItems(int count, out int overflow)
    {
        overflow = 0;
        if (!HasItemType)
        {
            return false;
        }

        if (ItemCount + count > StackSize)
        {
            overflow = count - RemainingSpace;
            ItemCount = StackSize;
            return true;
        }

        ItemCount += count;
        return true;
    }

    public bool RemoveItems(int count, out int underflow)
    {
        underflow = 0;
        if (!HasItemType)
        {
            return false;
        }

        if (ItemCount - count < 0)
        {
            underflow = count - ItemCount;
            ItemCount = 0;
            return true;
        }

        ItemCount -= count;
        return true;
    }

    public void ResetSlot(ItemType newType)
    {
        ItemType = newType;
        ItemCount = 0;
    }

    public bool EmptySlot(out int removedItems)
    {
        removedItems = 0;
        if (IsEmpty)
        {
            return false;
        }
        removedItems = ItemCount;
        ItemCount = 0;
        return true;
    }

    public bool CanStoreItemType(ItemType itemType)
    {
        return !HasItemType || ItemType == itemType;
    }

    public int RemainingSpace
    {
        get => StackSize - ItemCount;
    }

    public bool HasItemType
    {
        get => ItemType != ItemType.None;
    }

    public bool IsEmpty
    {
        get => ItemType == ItemType.None || ItemCount == 0;
    }

    public bool IsFull
    {
        get => ItemType != ItemType.None && ItemCount == StackSize;
    }
}
