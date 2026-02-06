using Constants;
using Initializers;
using Managers;
using Models.Data;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Init
{
    public class GameBootComponent : MonoBehaviour
    {
        public IEnumerator Start()
        {
            yield return Init();
            SceneManager.LoadSceneAsync(SceneNames.Game);
        }

        async private Task Init()
        {
            AsyncInitDataContainer.AsyncInitData = new AsyncInitData
            {
                Settings = await SettingsManager.GetSettings()
            };
        }
    }
}
