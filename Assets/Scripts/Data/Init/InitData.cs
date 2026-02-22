using UnityEngine;
using UnityEngine.InputSystem;

namespace Data.Init;

public class InitData
{
    public required InputAction mousePositionAction;
    public required InputAction mousePositionDeltaAction;
    public required Terrain terrain;
    public required GameObject[] worldObjectPrefabs;
    public required GameObject villagerPrefab;
    public required Material[] materials;
}
