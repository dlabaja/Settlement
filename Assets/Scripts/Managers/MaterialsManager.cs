using System.Linq;
using UnityEngine;

namespace Managers;

public class MaterialsManager
{
    private readonly Material[] _materials;
    
    public MaterialsManager(Material[] materials)
    {
        _materials = materials;
    }

    public Material GetByName(string name)
    {
        return _materials.First(material => material.name == name);
    }

    public static Material[] LoadAllMaterials()
    {
        return Resources.LoadAll<Material>("Materials");
    }
}
