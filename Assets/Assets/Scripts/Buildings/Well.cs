using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Well : Building, IEnterCollideable
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