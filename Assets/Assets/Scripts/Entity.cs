using UnityEngine;

namespace Assets.Scripts
{
    public class Entity : MonoBehaviour, ICustomObject
    {
        [SerializeField] private int water;

        public int GetWater()
        {
            return water;
        }

        public void SetWater(int count)
        {
            water += count;
        }

        public void ResetWater()
        {
            water = Const.MaxWater;
        }
    }
}