using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private static readonly Dictionary<Const.Item, int> GlobalInventory = new();
        private static TMP_Text _gInventoryText;
        private static readonly List<Entity> _entities = new();

        private void Start()
        {
            CustomObject.Spawn<Entity>();
            Time.timeScale = Const.GameSpeed;
            _gInventoryText = FindObjectsOfType<TMP_Text>().FirstOrDefault(x => x.name == "gInventoryText");

            /*foreach (var obj in FindObjectsOfType<CustomObject>())
            {
                foreach (var item in obj.GetInventory())
                    UpdateGlobalInventory(item);
            }*/
        }

        public static void AddEntity(Entity gm)
        {
            _entities.Add(gm);
        }

        private void FixedUpdate()
        {
            foreach (var entity in _entities)
            {
                if (Utils.RndTick(Const.WaterDecreaseChance)) entity.DecreaseWater();
                if (Utils.RndTick(Const.SleepDecreaseChance)) entity.DecreaseSleep();
            }
        }

        public static void UpdateGlobalInventory(KeyValuePair<Const.Item, int> item)
        {
            if (GlobalInventory.ContainsKey(item.Key))
            {
                GlobalInventory[item.Key] += item.Value;
                UpdateInvGui();
                return;
            }

            GlobalInventory.Add(item.Key, item.Value);
            UpdateInvGui();
        }

        public static void UpdateInvGui()
        {
            var s = "";
            foreach (var item in GlobalInventory) s += item.Key + ": " + item.Value;
            _gInventoryText.text = s;
        }
    }
}