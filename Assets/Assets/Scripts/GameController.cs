using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            //Utils.r = new Random();
            Utils.SpawnEntity();
            Utils.SpawnEntity();
            Utils.SpawnEntity();
            Utils.SpawnEntity();
        }

        private void FixedUpdate()
        {
            foreach (var entity in (Entity[]) FindObjectsOfType(typeof(Entity))) entity.DecreaseWater();
        }
    }
}