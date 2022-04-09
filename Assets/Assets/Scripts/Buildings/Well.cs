using UnityEngine;

namespace Assets.Scripts.Buildings
{
    public class Well : Building
    {
        private void OnCollisionEnter(Collision collision)
        {
            print("sus");

            if (collision.gameObject.GetComponent<Entity>().GetLookingFor().Contains(gameObject))
            {
                print("amogus");
                var entity = collision.gameObject.GetComponent<Entity>();
                entity.Stop(2000);
                entity.RefillWater();
                entity.HasColided?.Invoke(this, gameObject);
            }
        }
    }
}