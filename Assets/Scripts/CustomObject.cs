using UnityEngine;

public class CustomObject : MonoBehaviour
{
    protected internal static GameObject LoadGameObject(string path, string parentName)
    {
        var prefab = Resources.Load(path, typeof(GameObject));
        var parent = GameObject.Find(parentName).transform;
        return Instantiate(prefab, parent) as GameObject;
    }
    
    public static GameObject[] LoadGameObjects(string path, Const.Parent parentName, int count)
    {
        var prefab = Resources.Load(path, typeof(GameObject));
        var parent = GameObject.Find(parentName.ToString()).transform;
        var gms = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            gms[i] = Instantiate(prefab, parent) as GameObject;
        }

        return gms;
    }
}