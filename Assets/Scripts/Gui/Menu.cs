using Gui.Stats.Elements;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui
{
    public class Menu : MonoBehaviour
    {
        private void Awake()
        {
            var menu = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Menu");
            ((Button)menu.Q("Build").Children().First()).clicked += MenuBuild.ToggleOpen;
            ((Button)menu.Q("Paint").Children().First()).clicked += () =>
            {
                
            };
            ((Button)menu.Q("Territories").Children().First()).clicked += () =>
            {
                //todo territories
            };
            ((Button)menu.Q("CloseWindows").Children().First()).clicked += Stats.Stats.CloseAllStats;

        }
    }
}
