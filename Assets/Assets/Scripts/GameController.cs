using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            CustomObject.Spawn<Entity>();
        }

        private void FixedUpdate()
        {
            foreach (var entity in (Entity[]) FindObjectsOfType(typeof(Entity)))
                if (Utils.Rnd(Const.WaterDecreaseChance))
                    entity.DecreaseWater();
        }
    }
}