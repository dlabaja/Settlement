using Buildings;
using Buildings.Workplace;
using Gui.Stats;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class Entity : CustomObject, IStats
{
    public string Name { get; private set; }
    public Const.Gender Gender { get; private set; }
    public int Water { get; set; }
    public int Sleep { get; set; }
    [SerializeField] private GameObject workplace;
    [SerializeField] private GameObject house;
    private List<GameObject> lookingForBattery = new List<GameObject>();
    private GameObject lookingFor;
    private Inventory.Inventory _inventory;
    private NavMeshAgent _navMesh;

    public GameObject Workplace
    {
        get { return workplace; }
        set
        {
            workplace = value;
            EmptyInventory();
            SetDestinationToNextObject();
        }
    }

    public static Entity Spawn(string name = null)
    {
        var gm = LoadGameObject("Entity", "Entities").GetComponent<Entity>();
        gm.Name = name ?? GenerateName(GenerateGender());
        return gm;
    }

    private IEnumerator ElementaryNeeds()
    {
        while (true)
        {
            if (Water <= 0) AddDestination(FindNearestObject<Well>());
            else if (Sleep <= 0) AddDestination(house); //todo FindHouse()
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
            if (!lookingFor.activeSelf || (lookingFor == Workplace && !lookingForBattery.Any()))
                SetDestinationToNextObject();

            yield return new WaitForSeconds(1);
        }
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _inventory = GetComponent<Inventory.Inventory>();

        Gender = GenerateGender();
        Name ??= GenerateName(Gender);
        gameObject.name = Name;
        house = FindNearestObject<House>();
        Workplace = GameObject.Find(Const.Buildings.Spawn.ToString());
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
        if (lookingForBattery.Any())
        {
            lookingForBattery.RemoveAt(0);
            _navMesh.SetDestination(lookingFor!.transform.position);
            return;
        }

        Work();
    }

    //works until inventory is full, then finds workplace
    public void Work()
    {
        var workObjects = FindNearestObject(Workplace.GetComponent<Workplace>().WorkObject);
        if (_inventory.IsFull() || Workplace.GetComponent<Workplace>().WorkObject == Const.Buildings.None || !workObjects.Any()) //todo plnej itemů jiného typu (GetItemRoom?) - najít nejbližší skladiště co to obsahuje 
        {
            AddDestination(Workplace);
            return;
        }

        AddDestination(workObjects.FirstOrDefault(x => x != lookingFor));
    }

    // returns nearest object of type T and adds it to the lookingFor
    private GameObject FindNearestObject<T>() where T : CustomObject
    {
        var s = FindObjectsOfType<T>().OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).ToList().FirstOrDefault()!.gameObject;
        return s ? s : null;
    }

    // parses CustomObject enum to list of CustomObjects and returns its second or first item  
    private List<GameObject> FindNearestObject(Const.Buildings type)
    {
        if (type == Const.Buildings.None) return new List<GameObject>();
        var obj = FindObjectsOfType(
                Type.GetType("Buildings." + type) ?? Type.GetType("Buildings.Workplace." + type))
            .OrderBy(t => (((CustomObject)t).transform.position - transform.position).sqrMagnitude)
            .Cast<CustomObject>()
            .Select(x => x.gameObject).ToList();

        if (type == Const.Buildings.Tree)
            return obj.Take(6).OrderBy(_ => new Random().Next()).ToList();
        return obj;
    }

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
        if (buildings == null)
        {
            StartCoroutine(EmptyInventoryCoroutine());
            return;
        }

        foreach (var item in buildings!)
            AddDestination(item);
    }

    private IEnumerator EmptyInventoryCoroutine()
    {
        while (true)
        {
            if (!gameObject.activeInHierarchy) break;
            var buildings = _inventory.FindBuildingToEmptyInventory(_inventory);
            if (buildings == null) yield return new WaitForSeconds(1);
            else
            {
                AddDestination(buildings.First());
                break;
            }
        }
    }

    public GameObject GetLookingFor() => lookingFor;

    public List<GameObject> GetLookingForBattery() => lookingForBattery;

    public void RefillWater() => Water = 100;

    public void DecreaseWater() => Water = Mathf.Clamp(Water - 1, 0, 100);

    public void RefillSleep() => Sleep = 100;

    public void DecreaseSleep() => Sleep = Mathf.Clamp(Sleep - 1, 0, 100);

    public async Task Stop(int millis)
    {
        _navMesh.isStopped = true;
        await Task.Delay(millis / Const.GameSpeed);
        _navMesh.isStopped = false;
    }

    private static string GenerateName(Const.Gender gender)
    {
        return gender == Const.Gender.Male
            ? Const.MaleNames[new Random().Next(Const.MaleNames.Count)]
            : Const.FemaleNames[new Random().Next(Const.FemaleNames.Count)];
    }

    private static Const.Gender GenerateGender()
    {
        return new Random().Next(2) == 0 ? Const.Gender.Male : Const.Gender.Female;
    }

    public void GenerateStats()
    {
        Stats.GenerateStats(gameObject)
            .AddLabel(Name, 20)
            .AddLabel(Gender.ToString(), 17)
            .AddFocusDropdown(
                FindObjectsOfType<Workplace>().OrderBy(x => x.name)
                    .Where(x => !x.HasMaxWorkers() && x.gameObject != Workplace)
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
            .AddLabel(() => $"Sleep: {Sleep.ToString()}")
            .AddLabel(() => $"Water: {Water.ToString()}")
            .AddSpace()
            .AddLabel(() => Utils.DictToString(_inventory.GetInventory()))
            .BuildWindow();
    }
}
