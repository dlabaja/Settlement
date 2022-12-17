using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Buildings.Workplace
{
    public class Workplace : Building, IStats
    {
        //objects the entity has to meet (eg tree for woodcutter job)
        [SerializeField] private Const.CustomObjects workObjects;

        public Const.CustomObjects GetWorkObjects() => workObjects;
        public void DrawStats()
        {
            throw new NotImplementedException();
        }
    }
}
