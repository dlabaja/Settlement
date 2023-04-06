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
            if (gameObject.HasComponent<Workplace.Workplace>() && entity.Workplace != gameObject && entity.GetLookingFor() != gameObject) return;
            await Task.Delay(1000);
            await GetComponent<ICollideable>().OnCollision(entity);
            entity.SetDestinationToNextObject();
            if (entity.GetComponent<Inventory.Inventory>().IsFull())
                entity.AddDestination(entity.Workplace);
            if (!TryGetComponent<Warehouse>(out _) && entity.Workplace.HasComponent<Warehouse>())
            {
                inventory.TransferItems(inventory._startValues.FirstOrDefault().item,
                    Math.Clamp(inventory.GetItemCount(inventory._startValues.FirstOrDefault().item), 0, 5),
                    entity.gameObject,
                    gameObject);
                entity.SetDestinationToNextObject(); //Work
                print("t");
            }
        }
    }
}
