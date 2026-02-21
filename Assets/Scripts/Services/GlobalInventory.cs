using Enums;
using Models.Systems.Inventory;
using System;
using System.Collections.Generic;

namespace Services;

public class GlobalInventory
{
    private readonly Inventory _inventory = new Inventory(50, 1_000_000);
    private readonly List<Inventory> _inventories = new List<Inventory>();
    public event Action InventoryChanged;
    
    public void Register(Inventory inventory)
    {
        if (inventory.SlotCount == 0)
        {
            return;
        }
        _inventories.Add(inventory);
    }

    public void Remove(Inventory inventory)
    {
        _inventories.Remove(inventory);
    }

    public int GetItemCount(ItemType type)
    {
        return _inventory.GetItemCount(type);
    }

    public ItemType[] GetItemTypes()
    {
        return _inventory.GetItemTypes();
    }

    public void Update()
    {
        _inventory.Reset();
        foreach (var inventory in _inventories)
        {
            foreach (var itemType in inventory.GetItemTypes())
            {
                if (itemType == ItemType.None)
                {
                    continue;
                }
                _inventory.Add(inventory.GetItemCount(itemType), itemType, out _);
            }
        }
        InventoryChanged?.Invoke();
    }
}
