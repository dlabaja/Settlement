using Attributes;
using Models.Villager;
using Services;

namespace Factories;

public class VillagerFactory
{
    [Autowired] private VillagerConfigManager _configManager;

    public Villager Create()
    {
        var gender = _configManager.GetRandomGender();
        return new Villager(_configManager.GetRandomName(gender), gender);
    }
}
