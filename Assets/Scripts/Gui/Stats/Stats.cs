using Buildings;
using Buildings.Workplace;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gui.Stats
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
