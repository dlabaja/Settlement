using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Utils.LoadGameObject("Entity", Const.Parent.Entities);
        }
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