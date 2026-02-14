using Enums;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Models.Systems.Inventory;

public class InventorySlotArray
{
    public int SlotCount { get; }
    private InventorySlot[] _slots;

    public InventorySlotArray(int slotCount)
    {
        SlotCount = slotCount;
        _slots = ArrayUtils.CreateFilled(SlotCount, () => new InventorySlot(ItemType.None));
    }

    public void Add(ItemType itemType, int count, out int overflow)
    {
        overflow = count;
        var added = 0;
        foreach (var slot in GetSlotsForType(itemType))
        {
            if (count == 0)
            {
                return;
            }
        }
    }

    private IEnumerable<InventorySlot> GetSlotsForType(ItemType type)
    {
        return _slots.Where(slot => slot.CanStoreItemType(type));
    }
}
