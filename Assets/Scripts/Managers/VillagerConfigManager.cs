using Data.Villagers;
using Enums;
using Utils;

namespace Managers;

public class VillagerConfigManager
{
    private readonly VillagerNames _villagerNames;
    
    public VillagerConfigManager(VillagerNames villagerNames)
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
