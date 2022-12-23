using Assets.Scripts.Buildings;
using Assets.Scripts.Buildings.Workplace;
using UnityEngine;
using Assets.Scripts.Gui.Stats;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
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

        public static void GenerateStats(GameObject gm)
        {
            if (gm.HasComponent<Entity>())
            {
                Utils.LoadGameObject("Stats/Entity", Const.Parent.Canvas)
                    .GetComponent<EntityStats>().DrawEntityStats(gm.GetComponent<Entity>());
            }
            else if (gm.HasComponent<House>())
                print("dum stats");
            else if (gm.HasComponent<Workplace>())
                print("workplace stats");
        }

        public void CloseStats() => Destroy(gameObject);
    }
}
