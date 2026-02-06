using Managers;
using Reflex.Core;

namespace Initializers;

public class GameInitializer
{
    public async void Init(ContainerBuilder builder)
    {
        builder.RegisterValue(new SettingsManager(await SettingsManager.GetSettings()));
    }
}