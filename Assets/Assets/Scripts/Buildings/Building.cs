using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Building : CustomObject
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (OnCollision<IEnterCollideable>(collision))
                gameObject.GetComponent<IEnterCollideable>().OnCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (OnCollision<IStayCollideable>(collision) &&
                collision.gameObject.GetComponent<Entity>().GetLookingFor().Count == 0)
                gameObject.GetComponent<IStayCollideable>().OnCollision(collision);
        }

        private bool OnCollision<T>(Collision collision)
        {
            var e = collision.gameObject.GetComponent<Entity>();
            if (GetComponent<T>() == null || e == null) return false;

            if ((e.GetLookingFor().Contains(gameObject) || e.GetJob() == gameObject) &&
                collision.gameObject != Camera.main.gameObject) return true;
            return false;
        }
    }
}