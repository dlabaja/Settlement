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

        //[SerializeField] private GameObject house;
        //todo debug only
        [SerializeField] private List<GameObject> __lookingFor = new();
        private ObservableCollection<GameObject> _lookingFor = new();
        private NavMeshAgent _navMesh;

        private void Start()
        {
            _navMesh = GetComponent<NavMeshAgent>();

            gender = Utils.GenerateGender();
            name = Utils.GenerateName(gender);
            //todo rewrite to spawn
            workplace = FindObjectOfType<Woodcutter>().gameObject;

            _lookingFor.CollectionChanged += OnLookingForChanged;
            FindGameObject<Well>();
            _navMesh.SetDestination(_lookingFor.FirstOrDefault()!.transform.position);
        }

        //ondestroy zavolat onlookingforchanged
        
        //tasks the entity has to do
        private void OnLookingForChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add) return;

            if (water <= 0 && !HasGameObject<Well>()) FindGameObject<Well>();
            //TODO owning a house
            if (sleep <= 0 && !HasGameObject<House>()) FindGameObject<House>();
            if (_lookingFor.ToList().Count == 0)
            {
                print(_lookingFor.ToList().Count + "s" + __lookingFor.Count);
                _lookingFor.Add(workplace);
            }
            _navMesh.SetDestination(_lookingFor.FirstOrDefault()!.transform.position);
            __lookingFor = _lookingFor.ToList();
        }

        //removes item from lookingFor list, triggering OnLookingForChanged
        public void RemoveFromLookingFor(GameObject gm) => _lookingFor.Remove(gm);

        public IEnumerable<GameObject> GetLookingFor() => _lookingFor;

        //returns nearest object of type T
        public void FindGameObject<T>() where T : CustomObject
        {
            if (!HasGameObject<T>())
                _lookingFor.Add(FindObjectsOfType<T>()
                    .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                    .ToList().DefaultIfEmpty(workplace.GetComponent<CustomObject>())
                    .First().gameObject);
                    
        }

        //returns true if gameobject of type <T> is in the lookingFor list 
        public bool HasGameObject<T>()
        {
            return _lookingFor.Count(x => x.GetComponent<T>() is not null) != 0;
        }

        public int GetWater() => water;

        public void RefillWater() => water = 100;

        public void DecreaseWater() => water = Mathf.Clamp(water - 1, 0, 100);

        public int GetSleep() => sleep;

        public void RefillSleep() => sleep = 100;

        public void DecreaseSleep() => sleep = Mathf.Clamp(sleep - 1, 0, 100);

        public string GetName() => name;

        public Gender GetGender() => gender;

        public void SetJob(GameObject workplace)
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