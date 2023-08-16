using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class Utils
{
    private static readonly Random Rnd = new();

    public static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }

    public static bool RndTick(int max)
    {
        //game random ticks
        return Rnd.Next(max / Const.GameSpeed) == 0;
    }

    public static string DictToString<TK, TV>(Dictionary<TK, TV> dict, string keyValSeparator = ": ", string itemSeparator = "; ")
        => dict.Aggregate("", (current, item) => current + $"{item.Key}{keyValSeparator}{item.Value}{itemSeparator}");

    public static string ListToString<T>(List<T> ls) => string.Join(", ", ls);

    public static bool HasComponent<T>(this GameObject gm)
    {
        return gm.GetComponent<T>() is not null;
    }

    public static IEnumerator SlerpMove(Transform source, Vector3 posToMove, Action onComplete)
    {
        for (int i = 0; i < 400; i++)
        {
            source.position = Vector3.Slerp(source.position, posToMove, 0.02f);
            yield return new WaitForEndOfFrame();
        }

        onComplete.Invoke();
    }

    public static IEnumerator SlerpRotation(Transform source, Quaternion rotToMove)
    {
        for (int i = 0; i < 400; i++)
        {
            if (source.rotation == rotToMove) break;
            source.rotation = Quaternion.Slerp(source.rotation, rotToMove, 0.02f);
            yield return new WaitForEndOfFrame();
        }
    }

    public static Texture2D LoadTexture(string path)
    {
        Texture2D texture = new Texture2D(0, 0);
        texture.LoadImage(System.IO.File.ReadAllBytes(path));
        return texture;
    }

    public static string FormatProducing((List<Const.Item>, List<Const.Item>) producingItems)
    {
        if (producingItems.Equals(default))
            return null;

        StringBuilder str = new StringBuilder();
        foreach (Const.Item t in producingItems.Item1)
        {
            if (t == Const.Item.None) break;
            str.Append($"{t} ");
        }

        if (!producingItems.Item1.Contains(Const.Item.None))
            str.Append("-> ");

        foreach (Const.Item t in producingItems.Item2)
            str.Append($"{t} ");

        return str.ToString();
    }
}
