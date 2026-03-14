using Initializers;
using Reflex.Core;
using System;
using UnityEngine;

namespace Components.Init.Scopes
{
    public class CoreInitializerComponent : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            new CoreInitializer(builder).Init(BootDataContainer.BootData ?? throw new Exception("Client data not initialized"));
        }
    }
}
