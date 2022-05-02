using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            CustomObject.Spawn<Entity>();
            Time.timeScale = Const.GameSpeed;
        }

        private void FixedUpdate()
        {
            foreach (var entity in (Entity[]) FindObjectsOfType(typeof(Entity)))
                if (Utils.RndTick(Const.WaterDecreaseChance))
                    entity.DecreaseWater();
        }
    }
}