using Assets.Scripts.Buildings.Workplace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Stats
{
    public class EntityStats : Stats
    {
        private List<GameObject> dropdown = new List<GameObject>();

        public void DrawEntityStats(Entity entity)
        {
            var ui = gameObject.GetComponent<RectTransform>();
            var child = ui.transform.Find("Image");
            var name = child.Find("Name").GetComponent<Text>();
            var gender = child.Find("Gender").GetComponent<Text>();
            var lookingFor = child.Find("LookingFor").GetComponent<Text>();
            var workplace = child.Find("Workplace");
            var water = child.Find("Water").GetComponent<Text>();
            var sleep = child.Find("Sleep").GetComponent<Text>();

            name.text = entity.GetName();
            gender.text = entity.GetGender().ToString();

            UpdateDropdown(entity);

            workplace.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
            {
                var w = workplace.GetComponent<Dropdown>();

                //text mě nezajímá, jde o index (možná stačí list?)

                entity.Workplace = dropdown.ElementAt(w.value);
                print(entity.Workplace);
            });

            StartCoroutine(UpdateData(lookingFor, water, sleep, entity, workplace));
        }

        private void UpdateDropdown(Entity entity)
        {
            var dropdown_list = new List<GameObject>{entity.Workplace};
            foreach (var item in FindObjectsOfType<Workplace>())
            {
                if (entity.Workplace == item.gameObject) continue;
                dropdown_list.Add(item.gameObject);
            }

            foreach (var item in dropdown_list)
            {
                dropdown.Add(item);
            }
        }

        private IEnumerator UpdateData(Text lookingFor, Text water, Text sleep, Entity entity, Transform workplace)
        {
            while (true)
            {
                //todo house
                UpdateDropdown(entity);
                workplace.GetComponent<DropdownExt>().UpdateData(dropdown);

                lookingFor.text = Regex.Replace(entity.GetLookingFor().ToString(), @"\((.*?)\)", "");
                water.text = entity.GetWater().ToString();
                sleep.text = entity.GetSleep().ToString();

                yield return new WaitForSeconds(.5f);
            }
        }

        private void OnEntityWorkplaceChange()
        {

        }
    }
}
