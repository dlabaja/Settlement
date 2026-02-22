using Constants;
using Data.Init;
using Initializers;
using Instances;
using Reflex.Core;
using Services;
using System;
using UnityEngine;

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
            return ClientDataInitializer.ClientData ?? throw new Exception("AsyncInitData not loaded from the boot scene");
        }

        private InitData GetInitData()
        {
            var prefabs = PrefabsService.LoadAllPrefabs();

            return new InitData
            {
                mousePositionAction = InputActionMaps.Mouse.FindAction(InputActionName.MousePosition),
                mousePositionDeltaAction = InputActionMaps.Mouse.FindAction(InputActionName.MouseDelta),
                terrain = FindFirstObjectByType<Terrain>(),
                worldObjectPrefabs = prefabs.worldObjectPrefabs,
                villagerPrefab = prefabs.villagerPrefab,
                materials = MaterialsService.LoadAllMaterials(),
            };
        }
    }
}
