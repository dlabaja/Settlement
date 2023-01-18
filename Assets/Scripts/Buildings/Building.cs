using Interfaces;
using UnityEngine;

namespace Buildings
{
    public class Building : CustomObject
    {
        private void OnCollisionEnter(Collision collision) => OnTriggerEnter(collision.collider);

        private void OnTriggerEnter(Collider collider)
        {
            var entity = collider.gameObject.GetComponent<Entity>();
            if (entity.GetLookingFor() != gameObject) return;
            GetComponent<ICollideable>().OnCollision(collider.gameObject.GetComponent<Entity>());
            entity.ChangeLookingFor();
        }
    }
}
