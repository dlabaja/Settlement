namespace Models.Villager.VillagerStats;

public class VillagerStatsManager
{
    public VillagerStat Happiness { get; } = new VillagerStat();
    public VillagerStat Water { get; } = new VillagerStat();
    public VillagerStat Food { get; } = new VillagerStat();
    public VillagerStat Housing { get; } = new VillagerStat();
    public VillagerStat Church { get; } = new VillagerStat();
}
