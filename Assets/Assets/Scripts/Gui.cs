using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Gui : MonoBehaviour
    {
        private static TMP_Text _globalInventory;

        private void Start()
        {
            _globalInventory = FindObjectsOfType<TMP_Text>().FirstOrDefault(x => x.name == "globalInventory");
        }
        
        public static void SetGlobalInventoryText(string s) => _globalInventory.text = s;
    }
}