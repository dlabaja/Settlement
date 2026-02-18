using Attributes;
using Models.Villager;
using Services;

namespace Factories;

public class VillagerFactory
{
    [Autowired] private VillagerConfigService _configService;

    public Villager Create()
    {
        var gender = _configService.GetRandomGender();
        return new Villager(_configService.GetRandomName(gender), gender);
    }
}
