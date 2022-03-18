using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private void FixedUpdate()
        {
            foreach (var entity in FindObjectsOfType(typeof(Entity)))
            {
            }
        }
    }
}