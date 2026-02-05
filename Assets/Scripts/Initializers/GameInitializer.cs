using Configs;
using Managers;
using Reflex.Core;

namespace Initializers
{
    public class GameInitializer
    {
        public void Init(ContainerBuilder builder)
        {
            builder.RegisterValue(new SettingsManager(DefaultSettingsConfig.defaultSettings));
        }
    }
}
