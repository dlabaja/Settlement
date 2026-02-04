using Managers;
using Models.Data.Settings;
using Reflex.Core;

namespace Initializers
{
    public class GameInitializer
    {
        public static void Init(ContainerBuilder builder)
        {
            builder.RegisterValue(new SettingsManager(new SettingsModel()));
        }
    }
}
