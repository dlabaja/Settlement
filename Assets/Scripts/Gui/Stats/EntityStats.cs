using Assets.Scripts.Buildings.Workplace;
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
        public void DrawEntityStats(Entity entity)
        {
            var ui = gameObject.GetComponent<RectTransform>();
            var child = ui.transform.Find("Image");
            var name = child.Find("Name").GetComponent<Text>();
            var gender = child.Find("Gender").GetComponent<Text>();
            var lookingFor = child.Find("LookingFor").GetComponent<Text>();
            var wokplace = child.Find("Workspace").GetComponent<Dropdown>();
            var water = child.Find("Water").GetComponent<Text>();
            var sleep = child.Find("Sleep").GetComponent<Text>();
            
            name.text = entity.GetName();
            gender.text = entity.GetGender().ToString();

            StartCoroutine(UpdateData(lookingFor, water, sleep, entity, wokplace));
        }

        private IEnumerator UpdateData(Text lookingFor, Text water, Text sleep, Entity entity, Dropdown wokplace)
        {
            while (true)
            {
                //todo house
                wokplace.options.Clear();
                wokplace.options.Add(new Dropdown.OptionData(entity.Workplace.ToString()));
                foreach (var item in FindObjectsOfType<Workplace>())
                {
                    if (item.gameObject == entity.Workplace.gameObject) continue;
                    wokplace.options.Add(new Dropdown.OptionData(item.ToString()));
                }

                lookingFor.text = Regex.Replace(entity.GetLookingFor().ToString(), @"\((.*?)\)", "");
                water.text = entity.GetWater().ToString();
                sleep.text = entity.GetSleep().ToString();

                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
