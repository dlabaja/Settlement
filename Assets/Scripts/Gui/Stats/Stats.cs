using Buildings;
using Buildings.Workplace;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gui.Stats
{
    public class Stats : Window
    {
        protected GameObject _sender;

        private void NewStats(GameObject gm)
        {
            var ui = gameObject.GetComponent<RectTransform>();
            _sender = gm;

            var mouse = Mouse.current.position.ReadValue();
            ui.position = new Vector3(mouse.x, mouse.y, 0);
        }

        public static void GenerateStats(GameObject gm)
        {
            if (HasDuplicates(gm)) return; //only one stat per gameobject
            if (gm.HasComponent<Entity>())
                Utils.LoadGameObject("Stats/Entity", Const.Parent.Canvas)
                    .GetComponent<EntityStats>().NewStats(gm);
            else if (gm.HasComponent<House>())
                print("dum stats");
            else if (gm.HasComponent<Workplace>())
                Utils.LoadGameObject("Stats/Workplace", Const.Parent.Canvas)
                    .GetComponent<WorkplaceStats>().NewStats(gm);
        }

        private static bool HasDuplicates(GameObject gm) =>
            FindObjectsOfType<Stats>()
                .Select(x => x._sender == gm)
                .ToList().Count > 1;
    }
}
