using Reflex.Attributes;
using Services;
using System.Collections;
using UnityEngine;

namespace Components.Systems
{
    public class GlobalInventoryComponent : MonoBehaviour
    {
        [Inject] private GlobalInventoryService _globalInventoryService; 
        
        public void Awake()
        {
            StartCoroutine(UpdateInventory());
        }

        private IEnumerator UpdateInventory()
        {
            while (true)
            {
                _globalInventoryService.Update();
                yield return new WaitForSeconds(10);
            }
        }
    }
}
