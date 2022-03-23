using System;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Entity : CustomObject, ISpawnable
    {
        [SerializeField] private new string name;
        [SerializeField] private Const.Gender gender;
        [SerializeField] private int water;

        private void Awake()
        {
            NoWater += OnNoWater;
        }

        public void Spawn(GameObject prefab)
        {
            var entity = prefab.GetComponent<Entity>();
            entity.SetGender(Utils.GenerateGender());
            entity.SetName(Utils.GenerateName(entity.GetGender()));
            entity.FillWater();

            print(entity.GetName());
        }

        private event EventHandler NoWater;


        private void OnNoWater(object sender, EventArgs e)
        {
            print(GetName() + "no wotr");
        }

        public int GetWater()
        {
            return water;
        }

        public void SetWater(int water)
        {
            this.water += water;
        }

        public void FillWater()
        {
            water = 100;
        }

        public void DecreaseWater()
        {
            water = Mathf.Clamp(water - 1, 0, 100);
            if (water == 0) NoWater?.Invoke(this, EventArgs.Empty);
        }


        public void FindBuilding(GameObject gameObject)
        {
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Const.Gender GetGender()
        {
            return gender;
        }

        public void SetGender(Const.Gender gender)
        {
            this.gender = gender;
        }

        protected virtual void OnNoWater()
        {
            NoWater?.Invoke(this, EventArgs.Empty);
        }
    }
}