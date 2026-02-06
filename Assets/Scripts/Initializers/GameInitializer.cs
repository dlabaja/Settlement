using Managers;
using Models.Data;
using Reflex.Core;

namespace Initializers;

public class GameInitializer
{
    public void Init(ContainerBuilder builder, AsyncInitData data)
    {
        builder.RegisterValue(new SettingsManager(data.Settings));
    }
}