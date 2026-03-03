using UnityEngine;
using UnityEngine.AI;

namespace Services;

public class PathfindingService
{
    public bool CanReach(Vector3 start, Vector3 destination, out NavMeshPath path)
    {
        path = null;
        return NavMesh.CalculatePath(start, destination, NavMesh.AllAreas, path);
    }
}
