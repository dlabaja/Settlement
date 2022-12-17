using UnityEngine;
using Assets.Scripts.Gui.Stats;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Stats
{
    public class Stats : MonoBehaviour
    {
        private void Awake()
        {
            var ui = gameObject.GetComponent<RectTransform>();

            var mouse = Mouse.current.position.ReadValue();
            ui.position = new Vector3(mouse.x, mouse.y, 0);
        }

        public IEnumerator DrawEntityStats(Entity entity)
        {
            var ui = gameObject.GetComponent<RectTransform>();

            var child = ui.transform.Find("Image");
            var name = child.Find("Name").GetComponent<Text>();
            var gender = child.Find("Gender").GetComponent<Text>();
            var lookingFor = child.Find("LookingFor").GetComponent<Text>();
            var water = child.Find("Water").GetComponent<Text>();
            var sleep = child.Find("Sleep").GetComponent<Text>();

            name.text = entity.GetName();
            gender.text = entity.GetGender().ToString();
            
            while (gameObject != null)
            {
                UpdateData(entity, lookingFor, water, sleep);
                yield return new WaitForSeconds(.5f);
            }
        }

        private static void UpdateData(Entity entity, Text lookingFor, Text water, Text sleep)
        {
            //todo workplace, house
            lookingFor.text = Regex.Replace(entity.GetLookingFor().ToString(), @"\((.*?)\)", "");
            water.text = entity.GetWater().ToString();
            sleep.text = entity.GetSleep().ToString();
        }

        public void CloseStats()
        {
            print("uu");
            Destroy(gameObject);
        }
    }
}
