using Initializers;
using Reflex.Core;
using UnityEngine;

namespace Components.Init.Scopes
{
    public class GameInitializerComponent : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            BootDataContainer.InitGameData(Terrain.activeTerrain);
            new GameInitializer(builder).Init(BootDataContainer.BootData, BootDataContainer.GameData);
        }
    }
}
