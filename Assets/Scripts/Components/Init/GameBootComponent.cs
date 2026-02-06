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
            yield return ClientDataContainer.Init();
            SceneManager.LoadSceneAsync(SceneNames.Game);
        }
    }
}
