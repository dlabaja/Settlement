using UnityEngine;

namespace Assets.Scripts
{
    public class Window : MonoBehaviour
    {
        public void Close() => Destroy(gameObject);
    }
}
