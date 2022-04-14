using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : CustomObject
    {
        private void OnCollisionEnter(Collision collision)
        {
            var e = collision.gameObject.GetComponent<Entity>();
            if (e == null) return;

            if ((e.GetLookingFor().Contains(gameObject) ||
                 e.GetJob() == gameObject) &&
                collision.gameObject != Camera.main.gameObject)
                gameObject.GetComponent<ICollidable>().OnCollision(collision);
        }
    }
}