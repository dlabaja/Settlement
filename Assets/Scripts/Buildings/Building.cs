using Buildings.Workplace;
using Gui.Stats;
using Interfaces;
using Inventory;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Buildings
{
    public class Building : CustomObject
    {
        private void OnCollisionEnter(Collision collision) => OnTriggerEnter(collision.collider);

        async private void OnTriggerEnter(Collider collider)
        {
            var entity = collider.gameObject.GetComponent<Entity>();
            var inventory = GetComponent<Inventory.Inventory>();
            if (entity.GetLookingFor() != gameObject) return;
            if (gameObject.HasComponent<Workplace.Workplace>() && entity.Workplace != gameObject && entity.GetLookingFor() != gameObject) return;
            await GetComponent<ICollideable>().OnCollision(entity);
            entity.SetDestinationToNextObject();
            if (entity.GetComponent<Inventory.Inventory>().IsFull())
                entity.EmptyInventory();
            if (inventory.GetInventory().Values.Intersect(entity.neededItems).Any())
            {
                //todo requestování itemů, stavba budov
                var item = inventory.GetInventory().Values.Intersect(entity.neededItems).First();
                if (item.count >= inventory.GetItemCount(item.item))
                {
                    inventory.TransferItems(item.item,
                        item.count,
                        entity.gameObject,
                        gameObject);
                    entity.neededItems.RemoveAt(0);
                }
                else if (inventory.HasItem(item.item))
                {
                    inventory.TransferItems(item.item,
                        item.count,
                        entity.gameObject,
                        gameObject);
                    entity.neededItems[0] = new ItemStruct(item.item, item.count - inventory.GetItemCount(item.item));
                }
            }

            if (!TryGetComponent<Warehouse>(out _) && TryGetComponent<Workplace.Workplace>(out _) && entity.Workplace.HasComponent<Warehouse>()) //při kolizi transportera a workplacu
            {
                var item = inventory._startValues.FirstOrDefault().item;
                inventory.TransferItems(item,
                    Math.Clamp(inventory.GetItemCount(item), 0, 5),
                    entity.gameObject,
                    gameObject);
                entity.SetDestinationToNextObject();
                return;
            }

            await Task.Delay(1000);
        }
    }
}
