using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Workplace : Building
    {
        //objects the entity has to meet (eg tree for woodcutter job)
        [SerializeField] private Const.CustomObjects workObjects;
        [SerializeField] private List<GameObject> workers;
        [SerializeField] private int maxWorkers;
        [SerializeField] private List<Const.Item> _keys;
        [SerializeField] private List<int> _values;

        public Const.CustomObjects GetWorkObjects() => workObjects;

        public int GetMaxWorkers() => maxWorkers;

        public List<GameObject> GetWorkers() => workers;

        public bool AssignWorker(GameObject worker)
        {
            if (workers.Count >= maxWorkers) return false;
            workers.Add(worker);
            return true;
        }

        public void FireWorker(GameObject worker)
        {
            workers.Remove(worker);
        }

        public bool IsFull() => workers.Count == maxWorkers;
    }
}
