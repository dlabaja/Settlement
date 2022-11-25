using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : CustomObject
    {
        //todo waitTime
        private void OnCollisionStay(Collision collision)
        {
            OnTriggerStay(collision.collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            var entity = collider.gameObject.GetComponent<Entity>();
            //entity has building in lookingFor OR the lookingFor is empty and the building is its workspace
            if (entity.GetLookingFor() == gameObject ||
                entity.GetLookingFor() == null && entity.GetWorkplace == gameObject)
            {
                entity.ChangeLookingFor();
                GetComponent<ICollideable>().OnCollision(collider.gameObject.GetComponent<Entity>());
            }
        }
    }
}