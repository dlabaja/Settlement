using UnityEngine;

namespace Assets.Scripts
{
    public class Entity : MonoBehaviour, ICustomObject
    {
        [SerializeField] private new string name;
        [SerializeField] private Const.Gender gender;
        [SerializeField] private int water;

        public int GetWater()
        {
            return water;
        }

        public void SetWater(int water)
        {
            this.water += water;
        }

        public void FillWater()
        {
            water = Const.MaxWater;
        }

        public void DecreaseWater()
        {
            if (Utils.Rnd(Const.WaterDecreaseChance)) water--;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public Const.Gender GetGender()
        {
            return gender;
        }

        public void SetGender(Const.Gender gender)
        {
            this.gender = gender;
        }
    }
}