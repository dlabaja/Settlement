using Reflex.Core;
using Reflex.Enums;

namespace Initializers;

public abstract class Initializer
{
    private readonly ContainerBuilder _builder;

    protected Initializer(ContainerBuilder containerBuilder)
    {
        _builder = containerBuilder;
    }

    protected void RegisterService(object value)
    {
        _builder.RegisterValue(value);
    }
    
    protected void AutoRegisterService<T>()
    {
        _builder.RegisterType(typeof(T), Lifetime.Singleton, Resolution.Lazy);
    }
    
    protected void RegisterFactory<T>()
    {
        _builder.RegisterType(typeof(T), Lifetime.Singleton, Resolution.Lazy);
    }
}
