using Buildings.Workplace;
using Gui.Stats.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Gui.Stats
{
    public class EntityStats : Stats
    {
        public void Start()
        {
            // var entity = _sender.GetComponent<Entity>();
            // var ui = gameObject.GetComponent<RectTransform>();
            // var child = ui.transform.Find("Image");
            // var name = child.Find("Name").GetComponent<Text>();
            // var gender = child.Find("Gender").GetComponent<Text>();
            // var lookingFor = child.Find("LookingFor").GetComponent<Text>();
            // var workplace = child.Find("Workplace").GetComponent<FocusDropdown>();
            // var inventory = child.Find("Inventory").GetComponent<Text>();
            // var water = child.Find("Water").GetComponent<Text>();
            // var sleep = child.Find("Sleep").GetComponent<Text>();
            //
            // name.text = entity.GetName();
            // gender.text = entity.GetGender().ToString();
            //
            // workplace.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
            // {
            //     workplace.GetChosenElement().GetComponent<Workplace>().AssignWorker(entity);
            // });
            //
            // StartCoroutine(UpdateData(lookingFor, water, sleep, entity, workplace, inventory));
        }

        private static IEnumerator UpdateData(Text lookingFor, Text water, Text sleep, Entity entity, FocusDropdown workplace, Text inventory)
        {
            while (true)
            {
                // //todo house
                // workplace.UpdateData(
                //     FindObjectsOfType<Workplace>().OrderBy(x => x.name)
                //         .Where(x => !x.IsFull() && x.gameObject != entity.Workplace)
                //         .Select(x => x.gameObject)
                //         .Append(entity.Workplace.gameObject).ToList(), "");
                //
                // lookingFor.text = Regex.Replace(entity.GetLookingFor().ToString(), @"\((.*?)\)", "");
                //
                // inventory.text = Utils.DictToString(entity.GetComponent<Inventory.Inventory>().GetInventory());
                // if (string.IsNullOrEmpty(inventory.text)) inventory.text = "---";
                //
                // water.text = entity.GetWater().ToString();
                // sleep.text = entity.GetSleep().ToString();
                //
                // yield return new WaitForSeconds(.5f);
            }
        }
    }
}
