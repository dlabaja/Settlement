using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICollidable
    {
        public async void OnCollision(Collision collision)
        {
        }
    }
}