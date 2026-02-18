using Constants;
using Initializers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Init
{
    public class GameBootComponent : MonoBehaviour
    {
        public IEnumerator Start()
        {
            yield return ClientDataInitializer.Init();
            SceneManager.LoadSceneAsync(SceneName.Game);
        }
    }
}
