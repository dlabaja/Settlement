using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Buildings;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Entity : CustomObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Const.Gender gender;
        [SerializeField] private int water;
        [SerializeField] private int sleep;
        [SerializeField] private GameObject house;
        [SerializeField] private GameObject _job;
        [SerializeField] private List<GameObject> list;
        private readonly HashSet<GameObject> _lookingFor = new();
        private GameObject _interactingObject;
        private NavMeshAgent _navMesh;

        private GameObject Job
        {
            get => _job;
            set
            {
                _job = value;
                OnJobChanged();
            }
        }


        private void Start()
        {
            GameController.AddEntity(this);
            NoWater += OnNoWater;
            NoSleep += OnNoSleep;
            House.NewRoom += OnNewRoom;
            _navMesh = GetComponent<NavMeshAgent>();

            SetGender(Utils.GenerateGender());
            SetName(Utils.GenerateName(GetGender()));
            SetJob(GameObject.Find("Woodcutter"));
            FindHouse();
        }

        private void Update()
        {
            list = _lookingFor.ToList();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _interactingObject = collision.gameObject;
        }

        private void OnNewRoom(object sender, GameObject e)
        {
            if (house != null) return;
            house = e;
        }

        private void FindHouse()
        {
            foreach (var house in FindObjectsOfType<House>())
                if (house.capacity > house.GetOccupantsCount())
                    this.house = house.gameObject;
        }

        private void OnNoSleep(object sender, EventArgs e)
        {
            FindObject(house);
        }

        private event EventHandler NoSleep;

        private event EventHandler NoWater;

        public void FindJob()
        {
            var gm = Job ? Job : GameObject.Find("Spawn");
            _navMesh.SetDestination(gm.transform.position);
        }

        private void OnJobChanged()
        {
            FindJob();
        }

        public async Task Stop(int millis)
        {
            _navMesh.isStopped = true;
            await Task.Delay(millis / Const.GameSpeed);
            _navMesh.isStopped = false;
        }

        public void FindObject(GameObject gm)
        {
            AddToLookingFor(gm);
        }

        public void FindObject<T>() where T : CustomObject
        {
            var target = FindObjectsOfType<T>()
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                .ToList().FindAll(x => x != null && x.gameObject != _interactingObject).Take(2).ToList();

            if (!target.Any())
            {
                FindJob();
                return;
            }

            foreach (var item in target) AddToLookingFor(item.gameObject);
        }

        private void AddToLookingFor(GameObject gm)
        {
            if (gm == null) return;
            _lookingFor.Add(gm);
            OnLookingForChanged();
        }

        public void RemoveFromLookingFor(GameObject gm)
        {
            //if (gm == null || !_lookingFor.Contains(gm)) return;
            _lookingFor.RemoveWhere(x => x == gm);
            OnLookingForChanged();
        }

        private void OnLookingForChanged()
        {
            //_lookingFor.RemoveWhere(x => x == null);

            if (!_lookingFor.Any())
            {
                FindJob();
                return;
            }

            _navMesh.SetDestination(_lookingFor.FirstOrDefault()!.transform.position);
        }

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

        public void SetJob(GameObject workplace)
        {
            Job = workplace;
        }

        public int GetSleep()
        {
            return sleep;
        }

        public void RefillSleep()
        {
            sleep = 100;
        }

        public void DecreaseSleep()
        {
            sleep = Mathf.Clamp(sleep - 1, 0, 100);
            if (sleep == 0) NoSleep?.Invoke(this, EventArgs.Empty);
        }

        public GameObject GetJob()
        {
            return Job;
        }

        public HashSet<GameObject> GetLookingFor()
        {
            return _lookingFor;
        }
    }
}