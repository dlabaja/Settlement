using Buildings;
using Buildings.Workplace;
using Gui;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using static Const;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        
        Utils.LoadGameObjects("Entity", Parent.Entities, 7);

        //TODO Time.timeScale = Const.GameSpeed;
        MenuBuild.AddButton(typeof(Well), new BuildingPrice(new List<ItemStruct>{new ItemStruct(Item.Stone, 5)}, 10),"");
        MenuBuild.AddButton(typeof(Builder), new BuildingPrice(new List<ItemStruct>{new ItemStruct(Item.Wood, 5)}, 0),"");
        MenuBuild.AddButton(typeof(Woodcutter), new BuildingPrice(new List<ItemStruct>{new ItemStruct(Item.Tools, 5)}, 10),"");
        MenuBuild.AddButton(typeof(Stonecutter), new BuildingPrice(new List<ItemStruct>{new ItemStruct(Item.Tools, 5), new ItemStruct(Item.Wood, 10)}, 10),"");
        MenuBuild.AddButton(typeof(Gatherer), new BuildingPrice(new List<ItemStruct>{new ItemStruct(Item.Wood, 10)}, 10),"");
        MenuBuild.AddButton(typeof(Warehouse), new BuildingPrice(new List<ItemStruct>{new ItemStruct(Item.Stone, 10), new ItemStruct(Item.Planks, 15)}, 10),"");
    }

    private void FixedUpdate()
    {
        var entities = FindObjectsOfType<Entity>();
        foreach (var entity in entities)
        {
            if (Utils.RndTick(WaterDecreaseChance)) entity.DecreaseWater();
            if (Utils.RndTick(SleepDecreaseChance)) entity.DecreaseSleep();
        }
    }
}