using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Workplace : Building
    {
        //objects the entity has to meet (eg tree for woodcutter job)
        [SerializeField] private Const.CustomObjects workObjects;
        [SerializeField] private List<Entity> workers;
        [SerializeField] private int maxWorkers;

        public Const.CustomObjects GetWorkObjects() => workObjects;

        public int GetMaxWorkers() => maxWorkers;

        public List<Entity> GetWorkers() => workers;

        public bool AssignWorker(Entity worker)
        {
            if (workers.Count >= maxWorkers) return false;
            if (workers.Contains(worker)) return false;
            worker.Workplace.GetComponent<Workplace>().FireWorker(worker);
            workers.Add(worker);
            worker.Workplace = gameObject;
            return true;
        }

        public void FireWorker(Entity worker)
        {
            workers.Remove(worker);
            worker.GetComponent<Entity>().Workplace = GameObject.Find(Const.CustomObjects.Spawn.ToString());
        }

        public bool IsFull() => workers.Count == maxWorkers;
    }
}
