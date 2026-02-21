using Reflex.Attributes;
using Services;
using System.Collections;
using UnityEngine;

namespace Components.Systems
{
    public class GlobalInventoryComponent : MonoBehaviour
    {
        [Inject] private GlobalInventory _globalInventory; 
        
        public void Awake()
        {
            StartCoroutine(UpdateInventory());
        }

        private IEnumerator UpdateInventory()
        {
            while (true)
            {
                _globalInventory.Update();
                yield return new WaitForSeconds(10);
            }
        }
    }
}
