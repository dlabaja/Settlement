using Buildings.Workplace;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Time;

namespace Gui
{
    public class AssignDropdown : DropdownExt
    {
        public GameObject sender;

        private void UpdateDropdown(string firstItem)
        {
            var data = new List<Dropdown.OptionData>();
            data.AddRange(gameObjects
                .Select(x => new Dropdown.OptionData(x.ToString()))
                .Prepend(new Dropdown.OptionData(firstItem))
                .ToList());
            gameObject.GetComponent<Dropdown>().options = data;
        }
        
        public void UpdateData(List<GameObject> items, string firstItem)
        {
            gameObjects = items;
            UpdateDropdown(firstItem);
        }

        public void OnAssignClicked()
        {
            sender.GetComponent<Workplace>().AssignWorker(
                FindObjectsOfType<Entity>().FirstOrDefault(x => x.Workplace.name == Const.CustomObjects.Spawn.ToString())?.gameObject);
        }

        public void OnUnassignClicked()
        {
            sender.GetComponent<Workplace>().FireWorker(GetChosenElement());
        }
    }
}
