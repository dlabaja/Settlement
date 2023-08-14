using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestBase
    {
        protected Entity entity;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            var sceneLoadedTcs = new TaskCompletionSource<bool>();

            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            SceneManager.sceneLoaded += (arg0, mode) => sceneLoadedTcs.TrySetResult(true);
            yield return new WaitUntil(() => sceneLoadedTcs.Task.IsCompleted);
            
            entity = Entity.Spawn("Test");
        }
    }
}
