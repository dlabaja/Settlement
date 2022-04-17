using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : CustomObject
    {
        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerEnter(collision.collider);
        }


        //removes object from lookingFor when in the collision
        private void OnCollisionStay(Collision collision)
        {
            if (OnCollision(collision.gameObject))
                collision.gameObject.GetComponent<Entity>().HasColided?.Invoke(this, gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (OnCollision(collider.gameObject))
                gameObject.GetComponent<ICollideable>()
                    .OnCollision(collider.gameObject.GetComponent<Entity>());
        }

        private bool OnCollision(GameObject collision)
        {
            var e = collision.GetComponent<Entity>();
            if (GetComponent<ICollideable>() == null || e == null) return false;

            if ((e.GetLookingFor().Contains(gameObject) || e.GetJob() == gameObject) &&
                collision != Camera.main.gameObject) return true;
            return false;
        }
    }
}