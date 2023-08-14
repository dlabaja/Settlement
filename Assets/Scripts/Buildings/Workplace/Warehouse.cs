using Gui.Stats;
using Interfaces;
using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings.Workplace
{
    public class Warehouse : Workplace, ICollideable, IStats
    {
        private List<Entity> availableWorkers = new List<Entity>();
        public Dictionary<int, Const.WarehouseMode> itemMode = new Dictionary<int, Const.WarehouseMode>();

        //todo tlačítko #3 (zákaz braní itemů)   
        private void Awake()
        {
            MaxWorkers = 4;
            ProducingItems = (new List<Const.Item>{Const.Item.None}, new List<Const.Item>{Const.Item.None});
            
            itemMode = new Dictionary<int, Const.WarehouseMode>();
            OnWorkersChanged += () => availableWorkers = Workers;
            for (int i = 0; i < 4; i++)
                itemMode.Add(i, Const.WarehouseMode.ALLOW);
            InvokeRepeating(nameof(FindItemsToStore), 0f, 2f);
        }

        public async Task OnCollision(Entity entity)
        {
            var entInv = entity.GetComponent<Inventory.Inventory>();
            if (entity.Workplace.HasComponent<Warehouse>())
                gameObject.GetComponent<Inventory.Inventory>().TransferItems(
                    entInv.GetInventory().Values.FirstOrDefault().item,
                    entInv.GetInventory().Values.FirstOrDefault().count,
                    gameObject,
                    entity.gameObject);
            if (!availableWorkers.Contains(entity)) availableWorkers.Add(entity);
        }

        private void FindItemsToStore()
        {
            var index = -1;
            var inventory = new Dictionary<int, ItemStruct>(GetComponent<Inventory.Inventory>().GetInventory());
            foreach (var item in inventory
                         .Select(x => x.Value.item)
                         .Where(x => x != Const.Item.None))
            {
                index++;
                if (!availableWorkers.Any()) return;
                var worker = availableWorkers.First();
                if (itemMode[index] == Const.WarehouseMode.REJECT) continue;
                if (itemMode[index] == Const.WarehouseMode.UNSTOCK)
                {
                    var inv = GetComponent<Inventory.Inventory>();
                    var warehouse = FindObjectsOfType
                            <Warehouse>()
                        .Select(x => x.GetComponent<Inventory.Inventory>()).FirstOrDefault(x => x.HasRoomForItem(item) && x.gameObject != gameObject);
                    if (warehouse == null || inv.GetInventory()[index].count == 0 || new[]{Const.WarehouseMode.REJECT, Const.WarehouseMode.UNSTOCK}.Contains(warehouse.GetComponent<Warehouse>().itemMode[index])) continue;
                    inv.TransferItems(item,
                        Math.Clamp(inv.GetItemCount(item), 0, 5),
                        worker.gameObject,
                        gameObject);
                    worker.AddDestination(warehouse.gameObject);
                    availableWorkers.Remove(worker);
                    continue;
                }

                if (GetObjectsToTransfer(item).FirstOrDefault() == null) continue;
                worker.AddDestination(GetObjectsToTransfer(item).FirstOrDefault());
                availableWorkers.Remove(worker);
            }
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

        public bool RejectsItem(Const.Item itemToFind)
        {
            var index = 0;
            foreach (var item in GetComponent<Inventory.Inventory>().GetInventory().Values.Select(x => x.item))
            {
                if (item == itemToFind && itemMode[index] != Const.WarehouseMode.REJECT)
                    return false;
                index++;
            }

            return true;
        }

        public void SetItemMod(int index, Const.WarehouseMode mode) => itemMode[index] = mode;

        public new void GenerateStats()
        {
            Stats.GenerateStats(gameObject)
                .AddLabel(name, 20)
                .AddAssignDropdown()
                .AddWarehouseInventory()
                .AddLabel(() => Utils.DictToString(GetComponent<Inventory.Inventory>().GetInventory()))
                .BuildWindow();
        }
    }
}
