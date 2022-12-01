using Assets.Scripts.Buildings;
using Assets.Scripts.Buildings.Workplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Reflection;
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
            workplace = FindNearestObject<Woodcutter>();
            house = FindNearestObject<House>();

            ChangeLookingFor();
        }

        //updates tasks the entity has to do
        public void ChangeLookingFor()
        {
            lookingFor = null;
            if (water <= 0) SetDestination(FindNearestObject<Well>());
            //TODO owning a house
            else if (sleep <= 0) SetDestination(house);
            else if (lookingFor == null) Work();
        }

        public GameObject GetLookingFor() => lookingFor;

        //returns nearest object of type T and adds it to the lookingFor
        public GameObject FindNearestObject<T>() where T : CustomObject
        {
            try
            {
                return FindObjectsOfType<T>()
                    .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                    .ToList().FirstOrDefault()!.gameObject;
            }
            catch {}

            return null;
        }

        //parses CustomObject enum to list of CustomObjects and returns its second or first item  
        public GameObject FindNearestObject(Const.CustomObject type)
        {
            var s = FindObjectsOfType(Type.GetType("Assets.Scripts.Buildings." + type))
                .OrderBy(t => (((CustomObject)t).transform.position - transform.position).sqrMagnitude)
                .ToList();
            try
            {
                return (s[1] as CustomObject)?.gameObject;
            }
            catch
            {
                return WorkplaceOrDefault();
            }

        }

        //sets destination and adds it to the lookingFor, if null it finds workspace/spawn
        public void SetDestination(GameObject gm)
        {
            if (gm == null)
            {
                SetDestination(WorkplaceOrDefault());
                return;
            }

            _navMesh.SetDestination(gm.transform.position);
            lookingFor = gm;
        }

        //works until inventory is full, then finds workplace
        public void Work()
        {
            //if workspace == null
            var workObjects = workplace.GetComponent<Workplace>().GetWorkObjects();
            var inventory = gameObject.GetComponent<Inventory.Inventory>();

            if (inventory.IsFull())
            {
                SetDestination(WorkplaceOrDefault()); //todo vyprázdnit do skladu
                return;
            }

            SetDestination(FindNearestObject(workObjects));

            //z nějakýho listu workspacu zjistit kam chodit a co tam dělat
            //pracovat dokud se nenaplní inventář/nedojdou fyz. potřeby
            //po naplnění vyprázdnit ve worksapce, případně v přidruženém skladu
        }

        public GameObject WorkplaceOrDefault() => workplace ? workplace : FindObjectOfType<Spawn>().gameObject;

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

        public void SetWorkplace(GameObject workplace)
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
