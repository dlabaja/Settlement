using Constants;
using Data.Init;
using Initializers;
using Reflex.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Init
{
    public class GameInitializerComponent : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            var clientData = GetClientData();
            var initData = GetInitData();
            new GameInitializer().Init(builder, clientData, initData);
        }

        private ClientData GetClientData()
        {
            return ClientDataContainer.ClientData ?? throw new Exception("AsyncInitData not loaded from the boot scene");
        }

        private InitData GetInitData()
        {
            var mouseActionMap = InputSystem.actions.FindActionMap(InputActionMapName.Mouse);
            return new InitData
            {
                mousePositionAction = mouseActionMap.FindAction(InputActionName.MousePosition),
                mousePositionDeltaAction = mouseActionMap.FindAction(InputActionName.MouseDelta)
            };
        }
    }
}
