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
        private void Awake()
        {
            InvokeRepeating(nameof(FindItemsToStore), 0f, 5f);
        }

        public async Task OnCollision(Entity entity) //todo warehouse + inventář oncollision transferItems
        {
            var entInv = entity.GetComponent<Inventory.Inventory>();
            gameObject.GetComponent<Inventory.Inventory>().TransferItems(
                entInv.GetInventory().Values.FirstOrDefault().item,
                entInv.GetInventory().Values.FirstOrDefault().count,
                gameObject,
                entity.gameObject);
        }

        // async private Task TakeCareOfWork(Entity entity, GameObject target, Const.Item itemToGet)
        // {
        //     currentlyWorking.Add(entity);
        //     print(target);
        //     entity.AddDestination(target);
        //
        //     while (!entity.GetComponent<Collider>().bounds.Intersects(target.GetComponent<Collider>().bounds))
        //         await Task.Delay(50);
        //
        //     entity.GetComponent<Inventory.Inventory>().TransferItems(itemToGet,
        //         target.GetComponent<Inventory.Inventory>().GetItemCount(itemToGet),
        //         entity.gameObject,
        //         target);
        //
        //     entity.AddDestination(entity.Workplace);
        // }

        private void FindItemsToStore()
        {
            var index = 0;
            foreach (var item in GetComponent<Inventory.Inventory>()._startValues.Select(x => x.item))
            {
                if (item == Const.Item.None) continue;
                var objToTransfer = GetObjectsToTransfer(item);
                foreach (var i in objToTransfer)
                {
                    workers.ToArray()[index % workers.Count].AddDestination(i);
                }

                index++;
            }

            // foreach (var entity in gameObject.GetComponent<Workplace>().GetWorkers())
            // {
            //     if (currentlyWorking.Contains(entity))
            //         continue;
            //     TakeCareOfWork(entity, objToTransfer.Item1, objToTransfer.Item2);
            //     break;
            // }
        }

        private List<GameObject> GetObjectsToTransfer(Const.Item item)
        {
            var list = new List<GameObject>();
            foreach (var i in FindObjectsOfType<Workplace>().Where(x => !x.gameObject.TryGetComponent<Warehouse>(out _)))
            {
                if (!i.TryGetComponent<Inventory.Inventory>(out var gmInv))
                    continue;
                if (gmInv.HasItem(item))
                    list.Add(i.gameObject);
            }
            return list;
        }
        
        public new void GenerateStats()
        {
            Stats.GenerateStats(gameObject)
                .AddLabel(name, 20)
                .AddAssignDropdown()
                .AddLabelWithText("Items to store:", () => Utils.ListToString(GetComponent<Inventory.Inventory>()._startValues))
                .AddSpace()
                .AddLabel(() => Utils.DictToString(GetComponent<Inventory.Inventory>().GetInventory()))
                .BuildWindow();
        }
    }
}
