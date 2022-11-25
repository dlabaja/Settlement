using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Assets.Scripts.Buildings;
using UnityEngine;
using UnityEngine.AI;
using static Assets.Scripts.Const;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Assets.Scripts
{
    public class Entity : CustomObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Gender gender;
        [SerializeField] private int water;
        [SerializeField] private int sleep;

        //todo zaměstnanci
        [SerializeField] private GameObject workplace;

        [SerializeField] private GameObject house;
        [SerializeField] private GameObject lookingFor;
        private NavMeshAgent _navMesh;

        private void Start()
        {
            _navMesh = GetComponent<NavMeshAgent>();

            gender = Utils.GenerateGender();
            name = Utils.GenerateName(gender);
            //todo rewrite to spawn
            workplace = FindObjectsOfType<Woodcutter>()
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                .ToList().First().gameObject;

            FindGameObject<Well>();
        }

        //updates tasks the entity has to do
        public void ChangeLookingFor()
        {
            lookingFor = null;
            if (water <= 0) FindGameObject<Well>();
            //TODO owning a house
            else if (sleep <= 0) FindGameObject<House>();
            else if (lookingFor == null) SetDestination(workplace);
        }

        public GameObject GetLookingFor() => lookingFor;

        //returns nearest object of type T and adds it to the lookingFor
        public void FindGameObject<T>() where T : CustomObject
        {
            try
            {
                lookingFor = FindObjectsOfType<T>()
                    .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                    .ToList().FirstOrDefault()!.gameObject;
            }
            catch {}

            //goes to workplace
            if (lookingFor != null) SetDestination(lookingFor);
        }

        //sets destination and adds it to the lookingFor
        public void SetDestination(GameObject gm)
        {
            if (gm != null)
            {
                _navMesh.SetDestination(gm.transform.position);
                lookingFor = gm;
                return;
            }

            gm = workplace ? workplace : FindObjectOfType<Spawn>().gameObject;
            _navMesh.SetDestination(gm.transform.position);
            lookingFor = null;
        }

        public void FindHouse()
        {
            var houses = FindObjectsOfType<House>().ToList().Where(x => x.GetComponent<House>().HasFreeRoom());
            if (!houses.Any())
            {
                //todo postavit dům
            }

        }

        public int GetWater() => water;

        public void RefillWater() => water = 100;

        public void DecreaseWater() => water = Mathf.Clamp(water - 1, 0, 100);

        public int GetSleep() => sleep;

        public void RefillSleep() => sleep = 100;

        public void DecreaseSleep() => sleep = Mathf.Clamp(sleep - 1, 0, 100);

        public string GetName() => name;

        public Gender GetGender() => gender;

        public void SetWorkspace(GameObject workplace)
        {
            //sets job, event signal from Building
        }

        public GameObject GetWorkplace => workplace;

        public async Task Stop(int millis)
        {
            _navMesh.isStopped = true;
            await Task.Delay(millis / GameSpeed);
            _navMesh.isStopped = false;
        }
    }
}