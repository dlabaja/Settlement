using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Woodcutter : Building, IStayCollideable
    {
        public async void OnCollision(Collision collision)
        {
            await collision.gameObject.GetComponent<Entity>().Stop(1000);
            print("dřevo");
        }
    }
}