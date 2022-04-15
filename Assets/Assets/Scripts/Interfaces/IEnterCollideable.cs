using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IEnterCollideable
    {
        public void OnCollision(Collision collision);
    }
}