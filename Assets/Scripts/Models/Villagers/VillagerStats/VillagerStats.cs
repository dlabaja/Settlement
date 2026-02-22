namespace Models.Villagers.VillagerStats;

public class VillagerStats
{
    public VillagerStat Happiness { get; } = new VillagerStat(30);
    public VillagerStat Water { get; } = new VillagerStat(20);
    public VillagerStat Food { get; } = new VillagerStat(20);
    public VillagerStat Housing { get; } = new VillagerStat(20);
    public VillagerStat Church { get; } = new VillagerStat(20);
}
