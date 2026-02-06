using Initializers;
using Reflex.Core;
using System;
using UnityEngine;

namespace Components.Init
{
    public class GameInitializerComponent : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            var data = ClientDataContainer.ClientData;
            if (data == null)
            {
                throw new Exception("AsyncInitData not loaded from the boot scene");
            }
            new GameInitializer().Init(builder, data);
        }
    }
}
