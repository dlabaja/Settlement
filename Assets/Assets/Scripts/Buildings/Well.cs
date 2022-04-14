using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Buildings
{
    public class Well : Building, ICollidable
    {
        public async void OnCollision(Collision collision)
        {
            var entity = collision.gameObject.GetComponent<Entity>();
            await entity.Stop(2000);
            entity.RefillWater();
            entity.HasColided?.Invoke(this, gameObject);
        }
    }
}