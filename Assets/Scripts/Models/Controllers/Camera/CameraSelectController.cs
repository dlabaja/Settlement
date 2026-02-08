using Constants;
using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Controllers.Camera;

public class CameraSelectController
{
    public GameObject LastHighlighted;
    public GameObject LastSelected;
    public GameObject Highlighted;
    public GameObject Selected;
    private Dictionary<GameObject, Material> _originalMaterials;
    private Material _defaultMaterial;
    private Material _highlightMaterial;
    private Material _selectMaterial;

    public CameraSelectController(MaterialsManager materialsManager)
    {
        _defaultMaterial = materialsManager.GetByName(MaterialName.Default);
        _highlightMaterial = materialsManager.GetByName(MaterialName.Highlight);
        _selectMaterial = materialsManager.GetByName(MaterialName.Select);
        _originalMaterials = new Dictionary<GameObject, Material>();
    }
    
    public void Highlight(GameObject gameObject)
    {
        if (gameObject == Highlighted)
        {
            return;
        }
        
        LastHighlighted = Highlighted;
        Highlighted = gameObject;
        if (LastHighlighted)
        {
            ResetMaterial(LastHighlighted);
        }
        AddOriginalMaterial(Highlighted);
        SetMaterial(Highlighted, _highlightMaterial);
    }

    public void ResetHighlight()
    {
        if (!Highlighted)
        {
            return;
        }

        LastHighlighted = Highlighted;
        Highlighted = null;
        if (LastHighlighted)
        {
            ResetMaterial(LastHighlighted);
        }
    }
    
    private void AddOriginalMaterial(GameObject gameObject)
    {
        _originalMaterials.TryAdd(gameObject, gameObject.GetComponent<Renderer>().material);
    }

    private Material GetOriginalMaterial(GameObject gameObject)
    {
        var value = _originalMaterials.TryGetValue(gameObject, out var material);
        return value ? material : _defaultMaterial;
    }

    private void SetMaterial(GameObject gameObject, Material material)
    {
        gameObject.GetComponent<Renderer>().material = material;
    }
    
    private void ResetMaterial(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material = GetOriginalMaterial(gameObject);
    }
}
