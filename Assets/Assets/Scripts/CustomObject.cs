using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomObject : MonoBehaviour
    {

        private void FixedUpdate()
        {
            //foreach (var item in inventory) _inventory.Add(item.Key + ";" + item.Value);
        }

        public static void Spawn<T>()
        {
            LoadGameObject(typeof(T).Name, Utils.GetParent<T>().ToString());
        }

        

        public static GameObject LoadGameObject(string prefabName, string parentName)
        {
            return Instantiate(Resources.Load(prefabName, typeof(GameObject)),
                GameObject.Find(parentName).transform) as GameObject;
        }

        
    }
}