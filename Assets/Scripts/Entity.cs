using Buildings;
using Buildings.Workplace;
using System;
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
    private NavMeshAgent _navMesh;

    public GameObject Workplace
    {
        get { return workplace ? workplace : FindObjectOfType<Spawn>().gameObject; }
        set
        {
            try { workplace.GetComponent<Workplace>().FireWorker(gameObject); }
            catch {}

            if (!value.GetComponent<Workplace>().AssignWorker(gameObject)) return;
            workplace = value;
            Work();
        }
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();

        gender = Utils.GenerateGender();
        name = Utils.GenerateName(gender);
        house = FindNearestObject<House>();
        Workplace = GameObject.Find(Const.CustomObjects.Spawn.ToString());

        ChangeLookingFor();
    }

    //updates tasks the entity has to do
    public void ChangeLookingFor()
    {
        lookingFor = null;
        if (water <= 0) SetDestination(FindNearestObject<Well>());
        else if (sleep <= 0) SetDestination(house); //todo FindHouse()
        else if (lookingFor == null) Work();
    }

    //returns nearest object of type T and adds it to the lookingFor
    private GameObject FindNearestObject<T>() where T : CustomObject
    {
        try
        {
            return FindObjectsOfType<T>()
                .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
                .ToList().FirstOrDefault()!.gameObject;
        }
        catch {}

        return null;
    }

    //parses CustomObject enum to list of CustomObjects and returns its second or first item  
    private GameObject FindNearestObject(Const.CustomObjects type)
    {
        var s = FindObjectsOfType(Type.GetType("Buildings." + type))
            .OrderBy(t => (((CustomObject)t).transform.position - transform.position).sqrMagnitude)
            .ToList();
        try
        {
            return (s[1] as CustomObject)?.gameObject;
        }
        catch
        {
            return Workplace;
        }

    }

    //sets destination and adds it to the lookingFor, if null it finds workspace/spawn
    private void SetDestination(GameObject gm)
    {
        if (gm == null)
        {
            SetDestination(Workplace);
            return;
        }

        _navMesh.SetDestination(gm.transform.position);
        lookingFor = gm;
    }

    //works until inventory is full, then finds workplace
    private void Work()
    {
        if (Workplace.HasComponent<Spawn>())
        {
            SetDestination(GameObject.Find("Spawn"));
            return;
        }

        var workObjects = Workplace.GetComponent<Workplace>().GetWorkObjects();
        var inventory = gameObject.GetComponent<Inventory.Inventory>();

        if (inventory.IsFull())
        {
            SetDestination(Workplace); //todo vyprázdnit do skladu
            return;
        }

        SetDestination(FindNearestObject(workObjects));
        //todo check po naplnění vyprázdnit ve worksapce, případně v přidruženém skladu
    }

    public void FindHouse()
    {
        var houses = FindObjectsOfType<House>().Where(x => x.GetComponent<House>().HasFreeRoom());
        if (!houses.Any())
        {
            //todo postavit dům
        }
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