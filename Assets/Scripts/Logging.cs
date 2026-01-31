using UnityEngine;

public static class Logging
{
    public static void Log(params dynamic[] args)
    {
        Debug.Log(string.Join(", ", args));
    }
}