using Gui.Stats;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Buildings.Workplace
{
    public class Workplace : Building, IStats
    {
        //objects the entity has to meet (eg tree for woodcutter job)
        [SerializeField] private Const.CustomObjects workObject;
        [SerializeField] private List<Entity> workers;
        [SerializeField] private int maxWorkers;
        protected (List<Const.Item>, List<Const.Item>) producingItems;

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
        
        public bool IsFull() => workers.Count == maxWorkers;
        public void GenerateStats()
        {
            Stats.GenerateStats(gameObject)
                .AddLabel(name, 20)
                .AddAssignDropdown()
                .AddLabelWithTextVertical("Producing:", () => Utils.FormatProducing(producingItems))
                .AddSpace()
                .AddLabel(() => Utils.DictToString(GetComponent<Inventory.Inventory>().GetInventory()))
                .BuildStats();
        }
    }
}
