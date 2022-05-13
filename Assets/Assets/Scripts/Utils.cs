using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class Utils : MonoBehaviour
    {
        private static Random _rnd;

        private void Awake()
        {
            _rnd = new Random();
            Const.MaleNames = ((TextAsset) Resources.Load("male")).text.Split('\n');
            Const.FemaleNames = ((TextAsset) Resources.Load("female")).text.Split('\n');
        }

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
            return _rnd.Next(max / Const.GameSpeed) == 0;
        }

        public static int Rnd(int min, int max)
        {
            return _rnd.Next(min, max);
        }

        public static string GenerateName(Const.Gender gender)
        {
            return gender == Const.Gender.Male
                ? Const.MaleNames[_rnd.Next(Const.MaleNames.Length)]
                : Const.FemaleNames[_rnd.Next(Const.FemaleNames.Length)];
        }

        public static Const.Gender GenerateGender()
        {
            return _rnd.Next(2) == 0 ? Const.Gender.Male : Const.Gender.Female;
        }

        public static GameObject LoadGameObject(string prefabName, string parentName)
        {
            return Instantiate(Resources.Load(prefabName, typeof(GameObject)),
                GameObject.Find(parentName).transform) as GameObject;
        }

        public static Const.Parents GetParent<T>()
        {
            return typeof(T) == typeof(Entity) ? Const.Parents.Entities : Const.Parents.Buildings;
        }
    }
}