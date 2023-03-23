using Buildings;
using Buildings.Workplace;
using Gui;
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
        MenuBuild.AddButton(typeof(Well),"");
        MenuBuild.AddButton(typeof(Woodcutter),"");
        MenuBuild.AddButton(typeof(Stonecutter),"");
        MenuBuild.AddButton(typeof(Gatherer),"");
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