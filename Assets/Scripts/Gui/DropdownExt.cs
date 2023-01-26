using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class DropdownExt : MonoBehaviour
    {
        protected List<GameObject> gameObjects = new List<GameObject>();

        private void UpdateDropdown()
        {
            gameObject.GetComponent<Dropdown>().options = gameObjects
                .Select(x => new Dropdown.OptionData(x.ToString())).ToList();
        }

        public GameObject GetChosenElement()
        {
            return gameObjects.ElementAt(GetComponent<Dropdown>().value);
        }

        public void UpdateData(List<GameObject> items)
        {
            gameObjects = items;
            UpdateDropdown();
        }
    }
}
