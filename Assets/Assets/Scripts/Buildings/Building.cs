using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : CustomObject
    {
        private void OnCollisionStay(Collision collision)
        {
            OnTriggerStay(collision.collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            var entity = collider.gameObject.GetComponent<Entity>();
            if (entity.GetLookingFor().FirstOrDefault() == gameObject)
            {
                //smazat jenom jednou
                entity.RemoveFromLookingFor(gameObject);
                GetComponent<ICollideable>().OnCollision(collider.gameObject.GetComponent<Entity>());
            }
        }
    }
}