using Constants;
using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Controllers.Camera;

public class CameraSelectController
{
    private readonly Material _defaultMaterial;
    private readonly Material _highlightMaterial;
    private readonly Material _selectMaterial;
    private readonly Dictionary<Renderer, Material> _originalMaterials;
    public GameObject Highlighted;
    public GameObject Selected;

    public CameraSelectController(MaterialsManager materialsManager)
    {
        _defaultMaterial = materialsManager.GetByName(MaterialName.Default);
        _highlightMaterial = materialsManager.GetByName(MaterialName.Highlight);
        _selectMaterial = materialsManager.GetByName(MaterialName.Select);
        _originalMaterials = new Dictionary<Renderer, Material>();
    }

    public void Highlight(GameObject gameObject)
    {
        if (gameObject == Highlighted)
        {
            return;
        }
        
        HighlightRenderer(gameObject.GetComponent<Renderer>());
    }

    private void HighlightRenderer(Renderer renderer)
    {
        _originalMaterials.TryAdd(renderer, renderer.material);
        renderer.material = _highlightMaterial;
        ResetHighlight();
        Highlighted = renderer.gameObject;
    }
    
    public void Select(Renderer renderer)
    {
        _originalMaterials.TryAdd(renderer, renderer.material);
        renderer.material = _selectMaterial;
    }
    
    private void Reset(Renderer renderer)
    {
        renderer.material = _originalMaterials.ContainsKey(renderer) 
            ? _originalMaterials[renderer]
            : _defaultMaterial;
    }

    public void ResetHighlight()
    {
        if (Highlighted)
        {
            Reset(Highlighted.GetComponent<Renderer>());
            Highlighted = null;
        }
    }
}
