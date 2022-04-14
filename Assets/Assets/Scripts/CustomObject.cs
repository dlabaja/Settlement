using UnityEngine;

namespace Assets.Scripts
{
    public class CustomObject : MonoBehaviour
    {
        public static void Spawn<T>()
        {
            print(typeof(T).Name);
            Utils.LoadGameObject(typeof(T).Name, Utils.GetParent<T>().ToString());
        }
    }
}