using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IStayCollideable
    {
        public void OnCollision(Collision collision);
    }
}