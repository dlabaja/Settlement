using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Entity : CustomObject, ISpawnable
    {
        [SerializeField] private new string name;
        [SerializeField] private Const.Gender gender;
        [SerializeField] private int water;
        [SerializeField] private List<GameObject> lookingFor;

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

        public void FindObject<T>() where T : CustomObject
        {
            var navMesh = gameObject.GetComponent<NavMeshAgent>();
            var target = FindObjectsOfType<T>()
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
            if (target != null) navMesh.SetDestination(target.transform.position);
            navMesh.speed = 50 * Const.GameSpeed;
        }

        private event EventHandler NoWater;


        private void OnNoWater(object sender, EventArgs e)
        {
            FindObject<Well>();
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