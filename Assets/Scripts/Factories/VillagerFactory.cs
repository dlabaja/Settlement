using Models.Villagers;
using Services;

namespace Factories;

public class VillagerFactory
{
    private readonly VillagerConfigService _configService;

    public VillagerFactory(VillagerConfigService configService)
    {
        _configService = configService;
    }

    public Villager Create()
    {
        var gender = _configService.GetRandomGender();
        return new Villager(_configService.GetRandomName(gender), gender);
    }
}
