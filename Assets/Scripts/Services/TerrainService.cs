using UnityEngine;

namespace Services;

public class TerrainService
{
    private readonly Terrain _terrain;
    
    public TerrainService(Terrain terrain)
    {
        _terrain = terrain;
    }

    public Vector3 GroundVector3(Vector3 vector3)
    {
        vector3.y = _terrain.SampleHeight(vector3);
        return vector3;
    }
}
