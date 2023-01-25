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

        public Const.CustomObjects GetWorkObjects() => workObjects;

        public int GetMaxWorkers() => maxWorkers;

        public List<GameObject> GetWorkers() => workers;

        public bool AssignWorker(GameObject worker)
        {
            if (workers.Count >= maxWorkers) return false;
            if (workers.Contains(worker)) return false;
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
