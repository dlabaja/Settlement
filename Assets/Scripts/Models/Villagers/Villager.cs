using Enums;
using Models.Systems.Inventory;
using Models.Villagers.Places;
using Models.Villagers.Stats;
using Models.Villagers.Tasks;

namespace Models.Villagers;

public class Villager
{
    public string Name { get; }
    public Gender Gender { get; }
    public Inventory Inventory { get; } = new Inventory(1);
    public VillagerTasks Tasks { get; } = new VillagerTasks();
    public VillagerStats Stats { get; } = new VillagerStats();
    public VillagerPlaces Places { get; } = new VillagerPlaces();
    
    public Villager(string name, Gender gender)
    {
        Name = name;
        Gender = gender;
    }
}
