using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        CustomObject.Spawn<Entity>();
        //TODO Time.timeScale = Const.GameSpeed;
    }

    private void FixedUpdate()
    {
        var entities = FindObjectsOfType<Entity>();
        foreach (var entity in entities)
        {
            if (Utils.RndTick(Const.WaterDecreaseChance)) entity.DecreaseWater();
            if (Utils.RndTick(Const.SleepDecreaseChance)) entity.DecreaseSleep();
        }
    }
}