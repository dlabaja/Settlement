using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomObject : MonoBehaviour
    {
        [SerializeField] private string id;

        public static void Spawn(string prefabName, string parentName)
        {
            var prefab = Utils.LoadGameObject(prefabName, parentName);
            prefab.GetComponent<ISpawnable>().Spawn(prefab);
        }
    }
}