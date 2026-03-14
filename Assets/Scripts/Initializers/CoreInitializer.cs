using Data.Init;
using Reflex.Core;
using Services;

namespace Initializers;

public class CoreInitializer : Initializer
{
    public CoreInitializer(ContainerBuilder containerBuilder) : base(containerBuilder) {}

    public void Init(BootData bootData)
    {
        RegisterServices(bootData);
    }
    
    private void RegisterServices(BootData bootData)
    {
        RegisterService(new SettingsService(bootData.Settings));
    }
}
