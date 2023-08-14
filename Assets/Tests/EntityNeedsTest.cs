using Buildings;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EntityNeedsTest : TestBase
    {
        [UnityTest]
        public IEnumerator Water()
        {
            entity.Water = 0;
            yield return new WaitUntil(() => entity.GetLookingFor().HasComponent<Well>());
            yield return new WaitUntil(() => entity.Water == 100);
        }
        
        [UnityTest]
        public IEnumerator Sleep()
        {
            entity.Sleep = 0;
            yield return new WaitUntil(() => entity.GetLookingFor().HasComponent<House>());
            yield return new WaitUntil(() => entity.Sleep == 100);
        }
    }
}
