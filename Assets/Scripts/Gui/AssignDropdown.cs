using Buildings.Workplace;
using System;
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

        public void OnAssignClicked() => sender.GetComponent<Workplace>().AssignWorker(
            FindObjectsOfType<Entity>().FirstOrDefault(x => x.Workplace.name == Const.CustomObjects.Spawn.ToString()));

        public void OnUnassignClicked() => sender.GetComponent<Workplace>().FireWorker(GetChosenElement().GetComponent<Entity>());
    }
}
