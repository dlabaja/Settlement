using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static string GenerateName(Const.Gender gender)
    {
        return gender == Const.Gender.Male
            ? Const.MaleNames[Rnd.Next(Const.MaleNames.Count)]
            : Const.FemaleNames[Rnd.Next(Const.FemaleNames.Count)];
    }

    public static Const.Gender GenerateGender()
    {
        return Rnd.Next(2) == 0 ? Const.Gender.Male : Const.Gender.Female;
    }

    public static string DictToString<TK, TV>(Dictionary<TK, TV> dict, string keyValSeparator = " : ", string itemSeparator = "; ")
    {
        return dict.Aggregate("", (current, item) => current + $"{item.Key}{keyValSeparator}{item.Value}{itemSeparator}");
    }

    public static Const.Parent GetParent<T>()
    {
        return typeof(T) == typeof(Entity) ? Const.Parent.Entities : Const.Parent.Buildings;
    }

    public static GameObject LoadGameObject(string path, Const.Parent parentName)
    {
        return Object.Instantiate(Resources.Load(path, typeof(GameObject)),
            GameObject.Find(parentName.ToString()).transform) as GameObject;
    }

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
}