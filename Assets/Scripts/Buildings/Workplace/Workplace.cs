using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Buildings.Workplace
{
    public class Workplace : Building
    {
        //objects the entity has to meet (eg tree for woodcutter job)
        [SerializeField] private Const.CustomObjects workObject;
        [SerializeField] private List<Entity> workers;
        [SerializeField] private int maxWorkers;
        public Dictionary<List<Const.Item>, List<Const.Item>> producingItems;

        public delegate void WorkersChanged();
        public event WorkersChanged OnWorkersChanged;
        
        public Const.CustomObjects GetWorkObject() => workObject;

        public int GetMaxWorkers() => maxWorkers;

        public List<Entity> GetWorkers() => workers;

        public bool AssignWorker(Entity worker)
        {
            if (workers.Count >= maxWorkers
                || workers.Contains(worker)
                || worker == null) return false;
            try
            {
                worker.Workplace.GetComponent<Workplace>().FireWorker(worker);
            }
            catch {}

            workers.Add(worker);
            worker.Workplace = gameObject;
            OnWorkersChanged?.Invoke();
            return true;
        }

        public void FireWorker(Entity worker)
        {
            workers.Remove(worker);
            worker.GetComponent<Entity>().Workplace = GameObject.Find(Const.CustomObjects.Spawn.ToString());
            OnWorkersChanged?.Invoke();
        }
    }
}
