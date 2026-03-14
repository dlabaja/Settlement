using Constants;
using Initializers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Init.Boot
{
    public class GameBootComponent : MonoBehaviour
    {
        public IEnumerator Start()
        {
            yield return BootDataContainer.InitBootData();
            SceneManager.LoadSceneAsync(SceneName.Game);
        }
    }
}
