namespace Models.Systems.Inventory;

public class Inventory
{
    public int SlotCount { get; }

    public Inventory(int slotCount)
    {
        SlotCount = slotCount;
    }
}
