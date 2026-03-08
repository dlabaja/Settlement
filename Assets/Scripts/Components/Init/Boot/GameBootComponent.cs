using Constants;
using Initializers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Init.Boot
{
    // loads all the async configuration in the boot scene, fills the ClientDataContainer and switches to game
    public class GameBootComponent : MonoBehaviour
    {
        public IEnumerator Start()
        {
            yield return ClientDataInitializer.Init();
            SceneManager.LoadSceneAsync(SceneName.Game);
        }
    }
}
