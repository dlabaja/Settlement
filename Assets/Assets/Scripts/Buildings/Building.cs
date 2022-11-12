using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : CustomObject
    {
        public List<Const.Item> pickupableItems;

        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerEnter(collision.collider);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (OnCollision(collider.gameObject))
            {
                gameObject.GetComponent<ICollideable>()
                    .OnCollision(collider.gameObject.GetComponent<Entity>());

                if (GetComponent<IInventoryPickable>() != null)
                    GetComponent<IInventoryPickable>()
                        .PickItems(gameObject, collider.gameObject, pickupableItems);
                collider.gameObject.GetComponent<Entity>().RemoveFromLookingFor(gameObject);
            }
        }

        private void OnTriggerStay(Collider collider)
        {
            var entity = collider.gameObject.GetComponent<Entity>();

            if (entity == null) return;
            if (entity.GetLookingFor().Contains(gameObject)) entity.RemoveFromLookingFor(gameObject);
        }

        private bool OnCollision(GameObject collision)
        {
            var e = collision.GetComponent<Entity>();
            if (GetComponent<ICollideable>() == null || e == null) return false;

            if ((e.GetLookingFor().Contains(gameObject) || e.GetJob() == gameObject && !e.GetLookingFor().Any()) &&
                collision != Camera.main.gameObject) return true;
            return false;
        }
    }
}