using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class Hud : MonoBehaviour
    {
        //todo přepsat
        private static Text _globalInventory;

        private void Awake()
        {
            _globalInventory = FindObjectsOfType<Text>().
                FirstOrDefault(x => x.name == "globalInventory");
        }
        
        public static void SetGlobalInventoryText(string s) => _globalInventory.text = s;
    }
}