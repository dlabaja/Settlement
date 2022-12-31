using Interfaces;
using UnityEngine;

namespace Buildings
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
                entity.GetLookingFor() == null && entity.Workplace == gameObject)
            {
                entity.ChangeLookingFor();
                if (gameObject.HasComponent<ICollideable>())
                    GetComponent<ICollideable>().OnCollision(collider.gameObject.GetComponent<Entity>());
            }
        }
    }
}
