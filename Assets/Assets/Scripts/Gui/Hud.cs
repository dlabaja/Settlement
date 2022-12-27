using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Hud : MonoBehaviour
    {
        //todo přepsat
        private static Text _globalInventory;

        private void Start()
        {
            _globalInventory = FindObjectsOfType<Text>().
                FirstOrDefault(x => x.name == "globalInventory");
        }
        
        public static void SetGlobalInventoryText(string s) => _globalInventory.text = s;
    }
}