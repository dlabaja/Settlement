using Gui.Stats;
using Interfaces;
using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using static Const;

namespace Buildings.Workplace
{
    public class Workplace : Building, IStats
    {
        //objects the entity has to meet (eg tree for woodcutter job)
        public Const.Buildings WorkObject { get; protected set; }
        public List<Entity> Workers { get; internal set; } = new List<Entity>();
        public int MaxWorkers { get; protected set; }
        protected (List<Item>, List<Item>) ProducingItems { get; set; } = (new List<Item>{Item.None}, new List<Item>{Item.None});

        public delegate void WorkersChanged();
        public event WorkersChanged OnWorkersChanged;

        public void AssignWorker(Entity worker)
        {
            if (Workers.Count + 1 > MaxWorkers
                || Workers.Contains(worker)
                || worker == null) return;
            try
            {
                worker.Workplace.GetComponent<Workplace>().FireWorker(worker);
            }
            catch {}

            Workers.Add(worker);
            worker.Workplace = gameObject;
            OnWorkersChanged?.Invoke();
        }

        protected void Start()
        {
            StartCoroutine(CollectEntityInventories());
        }

        private IEnumerator CollectEntityInventories()
        {
            while (true)
            {
                var bounds = GetComponent<Collider>().bounds;
                Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale);
                //Gizmos.DrawWireCube(transform.position, transform.localScale);
                foreach (var entity in colliders
                             .Where(x => x.gameObject.HasComponent<Entity>() && x.GetComponent<Entity>().Workplace == gameObject)
                             .Select(x => x.GetComponent<Entity>()))
                {
                    var entityInv = entity.GetComponent<Inventory.Inventory>();
                    var inv = GetComponent<Inventory.Inventory>();
                    if (inv.HasRoomForItem(entityInv.GetInventory().Values.First().item))
                    {
                        inv.TransferItems(entityInv.GetInventory()[0].item,
                            entityInv.GetInventory()[0].count,
                            gameObject,
                            entity.gameObject);
                    }
                }

                yield return new WaitForSeconds(2);
            }
        }

        public void FireWorker(Entity worker)
        {
            Workers.Remove(worker);
            worker.GetComponent<Entity>().Workplace = GameObject.Find(Const.Buildings.Spawn.ToString());
            OnWorkersChanged?.Invoke();
        }

        public bool HasMaxWorkers() => Workers.Count == MaxWorkers;

        private void OnDestroy()
        {
            foreach (var entity in Workers)
                entity.Workplace = GameObject.Find("Spawn");
        }

        public void GenerateStats()
        {
            var stats = Stats.GenerateStats(gameObject)
                .AddLabel(name, 20)
                .AddAssignDropdown();
            if (ProducingItems != (new List<Item>{Item.None}, new List<Item>{Item.None}))
            {
                stats.AddLabelWithText("Producing:", () => Utils.FormatProducing(ProducingItems));
            }

            stats.AddSpace()
                .AddLabel(() => Utils.DictToString(GetComponent<Inventory.Inventory>().GetInventory()))
                .BuildWindow();
        }
    }
}
