using System;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Stats
{
    public class EntityStats : Stats
    {
        private void DrawEntityStats(Entity entity)
        {
            var ui = Utils.LoadGameObject("StatsEntity", Const.Parent.Canvas).GetComponent<RectTransform>();
            var mouse = Mouse.current.position.ReadValue();
            ui.position = new Vector3(mouse.x, mouse.y, 0);

            var child = ui.transform.Find("Image");
            var name = child.Find("Name").GetComponent<Text>();
            var gender = child.Find("Gender").GetComponent<Text>();
            var lookingFor = child.Find("LookingFor").GetComponent<Text>();
            var water = child.Find("Water").GetComponent<Text>();
            var sleep = child.Find("Sleep").GetComponent<Text>();

            new Thread(() =>
            {
                if (ui == null) return;
                print("uwu");

                name.text = entity.GetName();
                gender.text = entity.GetGender().ToString();
                //todo workplace, house
                lookingFor.text = Regex.Replace(entity.GetLookingFor().ToString(), @"\((.*?)\)", "");
                ;
                water.text = entity.GetWater().ToString();
                child.Find("Sleep").GetComponent<Text>().text = entity.GetSleep().ToString();
                Thread.Sleep(500);
            }).Start();
        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}
