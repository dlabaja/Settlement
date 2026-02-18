using Data.Villagers;
using Enums;
using Utils;

namespace Services;

public class VillagerConfigService
{
    private readonly VillagerNames _villagerNames;
    
    public VillagerConfigService(VillagerNames villagerNames)
    {
        _villagerNames = villagerNames;
    }

    public string GetRandomName(Gender gender)
    {
        return gender == Gender.Male
            ? RandomUtils.GetRandomArrayItem(_villagerNames.MaleNames)
            : RandomUtils.GetRandomArrayItem(_villagerNames.FemaleNames);
    }

    public Gender GetRandomGender()
    {
        return RandomUtils.GetRandomEnumItem<Gender>();
    }
}
