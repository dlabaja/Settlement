using UnityEngine;

namespace Gui
{
    public class Window : MonoBehaviour
    {
        
        public void Close() => Destroy(gameObject);
    }
}
