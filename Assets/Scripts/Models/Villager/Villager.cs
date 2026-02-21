using Enums;
using Models.Systems.Inventory;
using Models.Villager.VillagerPlaces;

namespace Models.Villager;

public class Villager
{
    public string Name { get; }
    public Gender Gender { get; }
    public Inventory Inventory { get; } = new Inventory(1);
    public VillagerTasks.VillagerTasks Tasks { get; } = new VillagerTasks.VillagerTasks();
    public VillagerStats.VillagerStats Stats { get; } = new VillagerStats.VillagerStats();
    public VillagerPlace Place { get; } = new VillagerPlace();
    
    public Villager(string name, Gender gender)
    {
        Name = name;
        Gender = gender;
    }
}
