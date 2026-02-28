using System.Linq;
using UnityEngine;

namespace Services.Resources;

public class MaterialsService
{
    private readonly Material[] _materials;
    
    public MaterialsService(Material[] materials)
    {
        _materials = materials;
    }

    public Material GetByName(string name)
    {
        return _materials.First(material => material.name == name);
    }

    public static Material[] LoadAllMaterials()
    {
        return UnityEngine.Resources.LoadAll<Material>("Materials");
    }
}
