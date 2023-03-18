using Gui.Stats;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Warehouse : Workplace, ICollideable, IStats
    {
        private List<Entity> currentlyWorking = new List<Entity>();

        private void Awake()
        {
            InvokeRepeating(nameof(OnNewWork), 0f, 5f);
        }

        public async Task OnCollision(Entity entity)
        {
            if (!currentlyWorking.Contains(entity)) return;

            var entInv = entity.GetComponent<Inventory.Inventory>();
            if (entInv.IsEmpty()) return;

            gameObject.GetComponent<Inventory.Inventory>().TransferItems(
                entInv.GetInventory().Values.FirstOrDefault().item,
                entInv.GetInventory().Values.FirstOrDefault().count,
                gameObject,
                entity.gameObject);
            currentlyWorking.Remove(entity);
        }

        async private Task TakeCareOfWork(Entity entity, GameObject target, Const.Item itemToGet)
        {
            currentlyWorking.Add(entity);
            entity.SetDestination(target);

            while (!entity.GetComponent<Collider>().bounds.Intersects(target.GetComponent<Collider>().bounds))
                await Task.Delay(50);

            entity.GetComponent<Inventory.Inventory>().TransferItems(itemToGet,
                target.GetComponent<Inventory.Inventory>().GetItemCount(itemToGet),
                entity.gameObject,
                target);

            entity.SetDestination(entity.Workplace);
        }

        private void OnNewWork()
        {
            var objToTransfer = GetObjectToTransfer();
            if (objToTransfer == (null, Const.Item.None))
                return;

            foreach (var entity in gameObject.GetComponent<Workplace>().GetWorkers())
            {
                if (currentlyWorking.Contains(entity))
                    continue;
                TakeCareOfWork(entity, objToTransfer.Item1, objToTransfer.Item2);
                break;
            }
        }

        private (GameObject, Const.Item) GetObjectToTransfer()
        {
            var inv = GetComponent<Inventory.Inventory>().GetInventory();
            foreach (var item in FindObjectsOfType<Workplace>().Where(x => !x.gameObject.HasComponent<Warehouse>()))
            {
                var gmInv = item.GetComponent<Inventory.Inventory>().GetInventory();
                foreach (var kv in gmInv)
                    if (inv.Values.Select(x => x.item).ToList().Contains(kv.Value.item)
                        && kv.Value.count > 0)
                        return (item.gameObject, kv.Value.item);
            }

            return (null, Const.Item.None);
        }
        
        public void GenerateStats()
        {
            Stats.GenerateStats(gameObject)
                .AddLabel(name, 20)
                .AddAssignDropdown()
                .AddLabelWithTextVertical("Items to store:", () => Utils.ListToString(GetComponent<Inventory.Inventory>()._startValues))
                .AddSpace()
                .AddLabel(() => Utils.DictToString(GetComponent<Inventory.Inventory>().GetInventory()))
                .BuildStats();
        }
    }
}
