using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Entity : CustomObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Const.Gender gender;
        [SerializeField] private int water;
        private readonly ObservableCollection<GameObject> _lookingFor = new();
        private GameObject _job;
        private NavMeshAgent _navMesh;
        public EventHandler<GameObject> HasColided;

        private GameObject Job
        {
            get => _job;
            set
            {
                _job = value;
                OnJobChanged();
            }
        }

        private void Awake()
        {
            NoWater += OnNoWater;
            HasColided += OnHasColided;
            _lookingFor.CollectionChanged += OnLookingForChanged;
            _navMesh = GetComponent<NavMeshAgent>();

            SetGender(Utils.GenerateGender());
            SetName(Utils.GenerateName(GetGender()));
            SetJob(GameObject.Find("Woodcutter"));
        }

        public void FindJob()
        {
            if (Job != null)
            {
                _navMesh.SetDestination(Job.transform.position);
                return;
            }

            _navMesh.SetDestination(GameObject.Find("Spawn").transform.position);
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
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).ToArray();
            if (!target.Any()) FindJob();

            for (var i = 0; i < target.Count(); i++)
                if (target[i] != null)
                    AddToLookingFor(target[i].gameObject);
        }

        private void OnHasColided(object sender, GameObject g)
        {
            print("sus");
            RemoveFromLookingFor(g);
        }

        private void AddToLookingFor(GameObject gm)
        {
            _lookingFor.Add(gm);
        }

        private void RemoveFromLookingFor(GameObject gm)
        {
            _lookingFor.Where(l => l == gm).ToList().All(i => _lookingFor.Remove(i));
        }

        private void OnLookingForChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (_lookingFor.Count != 0 && _lookingFor[0] != null)
            {
                _navMesh.SetDestination(_lookingFor[0].transform.position);
                return;
            }

            FindJob();
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

        public GameObject GetJob()
        {
            return Job;
        }

        public ObservableCollection<GameObject> GetLookingFor()
        {
            return _lookingFor;
        }
    }
}