using Interfaces;
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
            if (entity.GetLookingFor() != gameObject) return;
            if (gameObject.HasComponent<Workplace.Workplace>() && collider.GetComponent<Entity>().Workplace != gameObject) return;
            await Task.Delay(1000);
            await GetComponent<ICollideable>().OnCollision(collider.gameObject.GetComponent<Entity>());
            entity.Work();
        }
    }
}
