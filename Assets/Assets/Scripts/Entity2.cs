using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Assets.Scripts.Buildings;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Assets.Scripts
{
    public class Entity2 : CustomObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Const.Gender gender;
        [SerializeField] private int water;
        [SerializeField] private int sleep;

        [SerializeField] private GameObject job;

        //[SerializeField] private GameObject house;
        private ObservableCollection<GameObject> _lookingFor = new();
        private NavMeshAgent _navMesh;

        private void Start()
        {
            //TODO GameController.AddEntity(gameObject);
            NoWater += OnNoWater;
            NoSleep += OnNoSleep;
            House.NewRoom += OnNewRoom;
            _navMesh = GetComponent<NavMeshAgent>();

            _lookingFor.CollectionChanged += OnLookingForChanged;
        }

        //tasks the entity has to do
        private void OnLookingForChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (water <= 0) FindGameObject<Well>();
            //TODO owning a house
            else if (sleep <= 0) FindGameObject<House>();
            else FindGameObject(job);
        }

        //returns nearest object of type T
        public void FindGameObject<T>() where T : CustomObject =>
            _lookingFor.Add(FindObjectsOfType<T>()
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                .ToList().FindAll(x => x != null).First().gameObject);

        public void FindGameObject(GameObject gm) => _lookingFor.Add(gm);

        private void FixedUpdate()
        {
        }
    }
}