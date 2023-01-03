using Buildings.Workplace;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Gui.Stats
{
    public class EntityStats : Stats
    {
        public void DrawEntityStats(Entity entity)
        {
            var ui = gameObject.GetComponent<RectTransform>();
            var child = ui.transform.Find("Image");
            var name = child.Find("Name").GetComponent<Text>();
            var gender = child.Find("Gender").GetComponent<Text>();
            var lookingFor = child.Find("LookingFor").GetComponent<Text>();
            var workplace = child.Find("Workplace");
            var inventory = child.Find("Inventory").GetComponent<Text>();
            var water = child.Find("Water").GetComponent<Text>();
            var sleep = child.Find("Sleep").GetComponent<Text>();

            name.text = entity.GetName();
            gender.text = entity.GetGender().ToString();

            workplace.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
            {
                entity.Workplace = workplace.GetComponent<DropdownExt>().GetChosenElement();
            });

            StartCoroutine(UpdateData(lookingFor, water, sleep, entity, workplace, inventory));
        }

        private static IEnumerator UpdateData(Text lookingFor, Text water, Text sleep, Entity entity, Component workplace, Text inventory)
        {
            var dropdownExt = workplace.GetComponent<DropdownExt>();
            while (true)
            {
                //todo house
                dropdownExt.UpdateData(
                    FindObjectsOfType<Workplace>().OrderBy(x => x.name)
                        .Where(x => !x.IsFull())
                        .Select(x => x.gameObject).ToList());

                lookingFor.text = Regex.Replace(entity.GetLookingFor().ToString(), @"\((.*?)\)", "");
                
                inventory.text = Utils.DictToString(entity.GetComponent<Inventory.Inventory>().GetInventory());
                if (string.IsNullOrEmpty(inventory.text)) inventory.text = "---";
                
                water.text = entity.GetWater().ToString();
                sleep.text = entity.GetSleep().ToString();

                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
