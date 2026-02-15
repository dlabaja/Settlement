using Enums;
using Models.Systems.Inventory;
using Models.Villager.VillagerPlaces;
using Models.Villager.VillagerStats;
using Models.Villager.VillagerTasks;

namespace Models.Villager;

public class Villager
{
    public string Name { get; }
    public Gender Gender { get; }
    public Inventory Inventory { get; } = new Inventory(1);
    public VillagerTasksManager TasksManager { get; } = new VillagerTasksManager();
    public VillagerStatsManager StatsManager { get; } = new VillagerStatsManager();
    public VillagerPlaceManager PlaceManager { get; } = new VillagerPlaceManager();
    
    public Villager(string name, Gender gender)
    {
        Name = name;
        Gender = gender;
    }
}
