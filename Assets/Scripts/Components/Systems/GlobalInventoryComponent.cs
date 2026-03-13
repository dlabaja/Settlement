using Reflex.Attributes;
using Services.Systems;
using System.Collections;
using UnityEngine;

namespace Components.Systems
{
    public class GlobalInventoryComponent : MonoBehaviour
    {
        [Inject] private GlobalInventoryService _globalInventoryService; 

        public void Start()
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
