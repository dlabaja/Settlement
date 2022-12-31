using UnityEngine;

public class CustomObject : MonoBehaviour
{
    //spawn object
    public static void Spawn<T>() => Utils.LoadGameObject(typeof(T).Name, Utils.GetParent<T>());

}