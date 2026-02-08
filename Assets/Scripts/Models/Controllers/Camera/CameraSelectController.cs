using Constants;
using Managers;
using UnityEngine;

namespace Models.Controllers.Camera;

public class CameraSelectController
{
    private readonly Material defaultMaterial;
    private readonly Material highlightMaterial;
    private readonly Material selectMaterial;

    public CameraSelectController(MaterialsManager materialsManager)
    {
        defaultMaterial = materialsManager.GetByName(MaterialName.Default);
        highlightMaterial = materialsManager.GetByName(MaterialName.Highlight);
        selectMaterial = materialsManager.GetByName(MaterialName.Select);
    }

    public void Reset(Renderer renderer)
    {
        renderer.material = defaultMaterial;
    }

    public void Highlight(Renderer renderer)
    {
        renderer.material = highlightMaterial;
    }
    
    public void Select(Renderer renderer)
    {
        renderer.material = selectMaterial;
    }
}
