using Initializers;
using Reflex.Core;
using UnityEngine;

namespace Components
{
    public class GameInitializerComponent : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            new GameInitializer().Init(builder);
        }
    }
}
