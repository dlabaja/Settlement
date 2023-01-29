using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class DropdownExt : MonoBehaviour
    {
        private List<GameObject> gameObjects = new List<GameObject>();

        private void UpdateDropdown() =>
            gameObject.GetComponent<Dropdown>().options = gameObjects
                .Select(x => new Dropdown.OptionData(x.name))
                .ToList();

        public GameObject GetChosenElement() => gameObjects.ElementAt(GetComponent<Dropdown>().value);

        public void UpdateData(List<GameObject> items, string label)
        {
            gameObjects = items;
            gameObject.transform.Find("Text").GetComponent<Text>().text = label;
            UpdateDropdown();
        }
    }
}
