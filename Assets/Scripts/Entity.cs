using Buildings;
using Buildings.Workplace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Entity : CustomObject
{
    [SerializeField] private new string name;
    [SerializeField] private Const.Gender gender;
    [SerializeField] private int water;
    [SerializeField] private int sleep;
    [SerializeField] private GameObject workplace;
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject lookingFor;
    private Inventory.Inventory _inventory;
    private NavMeshAgent _navMesh;

    public GameObject Workplace
    {
        get { return workplace ? workplace : FindObjectOfType<Spawn>().gameObject; }
        set
        {
            try { EmptyInventory(workplace); }
            catch {}

            workplace = value;
            Work();
        }
    }

    private IEnumerator UnstuckMechanism()
    {
        while (true)
        {
            if (!lookingFor.activeSelf || lookingFor == null)
                Work();
            yield return new WaitForSeconds(1);
        }
    }
    
    private IEnumerator ElementaryNeeds()
    {
        while (true)
        {
            if (water <= 0) SetDestination(FindNearestObject<Well>());
            else if (sleep <= 0) SetDestination(house); //todo FindHouse()
            yield return new WaitForSeconds(10);
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
        StartCoroutine(UnstuckMechanism());
    }
    
    //works until inventory is full, then finds workplace
    public void Work()
    {
        if (_inventory.IsFull() || Workplace.GetComponent<Workplace>().GetWorkObject() == Const.CustomObjects.None) //todo plnej itemů jiného typu (GetItemRoom?) - najít nejbližší skladiště co to obsahuje 
        {
            SetDestination(Workplace);
            return;
        }

        var workObjects = FindNearestObject(Workplace.GetComponent<Workplace>().GetWorkObject());
        SetDestination(workObjects.FirstOrDefault(x => x != lookingFor));
    }

    //returns nearest object of type T and adds it to the lookingFor
    private GameObject FindNearestObject<T>() where T : CustomObject
    {
        var s = FindObjectsOfType<T>().OrderBy(t => (t.transform.position - transform.position).sqrMagnitude).ToList().FirstOrDefault()!.gameObject;
        return s ? s : null;
    }

    //parses CustomObject enum to list of CustomObjects and returns its second or first item  
    private IEnumerable<GameObject> FindNearestObject(Const.CustomObjects type) => FindObjectsOfType(
            Type.GetType("Buildings." + type) ?? Type.GetType("Buildings.Workplace." + type))
        .OrderBy(t => (((CustomObject)t).transform.position - transform.position).sqrMagnitude)
        .Cast<CustomObject>()
        .Select(x => x.gameObject);

    //sets destination and adds it to the lookingFor, if null it finds workspace/spawn
    public void SetDestination(GameObject gm)
    {
        if (gm == null)
            gm = Workplace;

        _navMesh.SetDestination(gm.transform.position);
        lookingFor = gm;
    }

    public void FindHouse()
    {
        var houses = FindObjectsOfType<House>().Where(x => x.GetComponent<House>().HasFreeRoom());
        if (!houses.Any())
        {
            //todo postavit dům
        }
    }

    private void EmptyInventory(GameObject gm)
    {
        SetDestination(gm);
        _inventory.TransferItems(_inventory.GetInventory()[0].item, _inventory.GetInventory()[0].count, gm);
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
}
