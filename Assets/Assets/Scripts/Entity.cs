using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Assets.Scripts.Buildings;
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
        private readonly ObservableCollection<GameObject> _lookingFor = new ObservableCollection<GameObject>();
        private NavMeshAgent _navMesh;
        public EventHandler<GameObject> HasColided;


        private void Awake()
        {
            NoWater += OnNoWater;
            HasColided += OnHasColided;
            _lookingFor.CollectionChanged += OnLookingForChanged;
            _navMesh = GetComponent<NavMeshAgent>();
        }

        private void FixedUpdate()
        {
            print(_lookingFor.Count);
        }

        public void Spawn(GameObject prefab)
        {
            var entity = prefab.GetComponent<Entity>();
            entity.SetGender(Utils.GenerateGender());
            entity.SetName(Utils.GenerateName(entity.GetGender()));

            print(entity.GetName());
        }

        public void Stop(int millis)
        {
            _navMesh.isStopped = true;


            //StartCoroutine(Utils.Wait(millis));
            _navMesh.isStopped = false;
        }

        public ObservableCollection<GameObject> GetLookingFor()
        {
            return _lookingFor;
        }

        private void OnHasColided(object sender, GameObject g)
        {
            _lookingFor.Remove(g);
        }

        private void OnLookingForChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            foreach (var item in _lookingFor)
                if (item.GetComponent<Spawn>() != null)
                    _lookingFor.Remove(item);

            if (_lookingFor.Count == 0)
            {
                _navMesh.SetDestination(FindObjectOfType<Spawn>().transform.position);
                return;
            }

            _navMesh.SetDestination(_lookingFor[0].transform.position);
            _navMesh.speed = 50 * Const.GameSpeed;
        }

        public void FindObject<T>() where T : CustomObject
        {
            var target = FindObjectsOfType<T>()
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).FirstOrDefault();
            if (target == null) return;

            _lookingFor.Add(target.gameObject);
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

        public void RefillWater()
        {
            water = 100;
        }


        //if water is < 0, OnNoWater event is called
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
    }
}