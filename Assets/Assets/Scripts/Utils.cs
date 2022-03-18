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

        public static bool Rnd(int max)
        {
            //game random ticks
            if (_rnd.Next(max / Const.GameSpeed) == 0) return true;
            return false;
        }

        public static void SpawnEntity()
        {
            //new entity
            var prefab = Instantiate(Resources.Load("Entity", typeof(GameObject)),
                GameObject.Find("Entities").transform) as GameObject;

            if (prefab != null)
            {
                var entity = prefab.GetComponent<Entity>();
                entity.SetGender(GenerateGender());
                entity.SetName(GenerateName(entity.GetGender()));
                entity.FillWater();
            }
        }

        public static string GenerateName(Const.Gender gender)
        {
            if (gender == Const.Gender.Male) return Const.MaleNames[_rnd.Next(Const.MaleNames.Length)];
            return Const.FemaleNames[_rnd.Next(Const.FemaleNames.Length)];
        }

        public static Const.Gender GenerateGender()
        {
            if (_rnd.Next(2) == 0)
                return Const.Gender.Male;
            return Const.Gender.Female;
        }
    }
}