using Buildings;
using Buildings.Workplace;
using Gui.Stats;
using Gui.Stats.Elements;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Entity : CustomObject, IStats
{
    [SerializeField] private new string name;
    [SerializeField] private Const.Gender gender;
    [SerializeField] private int water;
    [SerializeField] private int sleep;
    [SerializeField] private GameObject workplace;
    [SerializeField] private GameObject house;
    public List<GameObject> lookingForBattery;
    public GameObject lookingFor;
    private Inventory.Inventory _inventory;
    private NavMeshAgent _navMesh;

    public GameObject Workplace
    {
        get { return workplace ? workplace : FindObjectOfType<Spawn>().gameObject; }
        set
        {
            EmptyInventory();
            workplace = value;
            Work();
        }
    }

    private IEnumerator ElementaryNeeds()
    {
        while (true)
        {
            if (water <= 0) AddDestination(FindNearestObject<Well>());
            else if (sleep <= 0) AddDestination(house); //todo FindHouse()
            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator CheckLookingFor()
    {
        while (true)
        {
            lookingForBattery = lookingForBattery
                .GroupBy(x => x)
                .Where(g => g.Count() == 1 && g.Key.activeSelf)
                .Select(g => g.Key).ToList();
            if (lookingFor is null || !lookingFor.activeSelf || (lookingFor == Workplace && !lookingForBattery.Any()))
                Work();

            yield return new WaitForSeconds(1);
        }
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _inventory = GetComponent<Inventory.Inventory>();

        gender = Utils.GenerateGender();
        name = Utils.GenerateName(gender);
        gameObject.name = name;
        house = FindNearestObject<House>();
        Workplace = GameObject.Find(Const.CustomObjects.Spawn.ToString());
        RefillWater();
        RefillSleep();

        StartCoroutine(ElementaryNeeds());
        StartCoroutine(CheckLookingFor());
    }

    public void AddDestination(GameObject gm)
    {
        if (gm == null) return;
        lookingForBattery.Add(gm);
        if (lookingForBattery.Count == 1)
        {
            SetDestinationToNextObject();
        }
    }

    public void SetDestinationToNextObject()
    {
        lookingFor = lookingForBattery.FirstOrDefault();
        if (lookingFor != default)
        {
            lookingForBattery.RemoveAt(0);
            _navMesh.SetDestination(lookingFor!.transform.position);
        }
    }

    //works until inventory is full, then finds workplace
    public void Work()
    {
        var workObjects = FindNearestObject(Workplace.GetComponent<Workplace>().GetWorkObject());
        if (_inventory.IsFull() || Workplace.GetComponent<Workplace>().GetWorkObject() == Const.CustomObjects.None || !workObjects.Any()) //todo plnej itemů jiného typu (GetItemRoom?) - najít nejbližší skladiště co to obsahuje 
        {
            AddDestination(Workplace);
            return;
        }
        
        AddDestination(workObjects.FirstOrDefault(x => x != lookingFor));
    }

    //returns nearest object of type T and adds it to the lookingFor
    private GameObject FindNearestObject<T>() where T : CustomObject
    {
        var s = FindObjectsOfType<T>().OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).ToList().FirstOrDefault()!.gameObject;
        return s ? s : null;
    }

    //parses CustomObject enum to list of CustomObjects and returns its second or first item  
    private List<GameObject> FindNearestObject(Const.CustomObjects type) => FindObjectsOfType(
            Type.GetType("Buildings." + type) ?? Type.GetType("Buildings.Workplace." + type))
        .OrderBy(t => (((CustomObject)t).transform.position - transform.position).sqrMagnitude)
        .Cast<CustomObject>()
        .Select(x => x.gameObject).ToList();

    public void FindHouse()
    {
        var houses = FindObjectsOfType<House>().Where(x => x.GetComponent<House>().HasFreeRoom());
        if (!houses.Any())
        {
            //todo postavit dům
        }
    }

    public void EmptyInventory()
    {
        var buildings = _inventory.FindBuildingToEmptyInventory(_inventory);
        if (buildings == null) return;
        foreach (var item in buildings!)
            AddDestination(item);
    }

    public GameObject GetLookingFor() => lookingFor;

    public int GetWater() => water;

    public void RefillWater() => water = 100;

    public void DecreaseWater() => water = Mathf.Clamp(water - 1, 0, 100);

    public int GetSleep() => sleep;

    public void RefillSleep() => sleep = 100;

    public void DecreaseSleep() => sleep = Mathf.Clamp(sleep - 1, 0, 100);

    public string GetName() => name;

    public Const.Gender GetGender() => gender;

    public async Task Stop(int millis)
    {
        _navMesh.isStopped = true;
        await Task.Delay(millis / Const.GameSpeed);
        _navMesh.isStopped = false;
    }

    public void GenerateStats()
    {
        Stats.GenerateStats(gameObject)
            .AddLabel(name, 20)
            .AddLabel(gender.ToString(), 17)
            .AddFocusDropdown(
                FindObjectsOfType<Workplace>().OrderBy(x => x.name)
                    .Where(x => !x.IsFull() && x.gameObject != Workplace)
                    .Select(x => x.gameObject)
                    .Prepend(Workplace).ToList(),
                "Workplace"
            )
            .AddLabelWithText("Looking for:",
                () =>
                {
                    try
                    {
                        return lookingFor.name;
                    }
                    catch
                    {
                        return "";
                    }
                })
            .AddLabel(() => $"Sleep: {sleep.ToString()}")
            .AddLabel(() => $"Water: {water.ToString()}")
            .AddSpace()
            .AddLabel(() => Utils.DictToString(_inventory.GetInventory()))
            .BuildWindow();
    }
}
