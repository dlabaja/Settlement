using Buildings.Workplace;
using Interfaces;
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
            if (inventory.IsFull())
                entity.AddDestination(entity.Workplace);
            if (!TryGetComponent<Warehouse>(out _) && entity.Workplace.HasComponent<Warehouse>())
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
