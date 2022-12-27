using Assets.Scripts.Buildings;
using Assets.Scripts.Buildings.Workplace;
using UnityEngine;
using Assets.Scripts.Gui.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.Gui.Stats
{
    public class Stats : Window
    {
        private GameObject _sender;
        public GameObject GetSender() => _sender;
        private void Awake()
        {
            var ui = gameObject.GetComponent<RectTransform>();

            var mouse = Mouse.current.position.ReadValue();
            ui.position = new Vector3(mouse.x, mouse.y, 0);
        }

        public static void GenerateStats(GameObject gm)
        {
            if(CheckDuplicates(gm)) return; //only one stat per gameobject
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

        public static bool CheckDuplicates(GameObject gm) => 
            FindObjectsOfType<Stats>()
            .Select(x => x.GetSender() == gm)
            .ToList().Count != 0;
    }
}
