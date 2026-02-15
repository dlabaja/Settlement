using Attributes;
using Managers;
using Models.Villager;

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
