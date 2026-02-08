using Constants;
using Managers;
using Models.Controllers.Camera;
using System.Collections.Generic;
using UnityEngine;

namespace Views;

public class CameraSelectView
{
    private Dictionary<GameObject, Material> _originalMaterials;
    private Material _defaultMaterial;
    private Material _highlightMaterial;
    private Material _selectMaterial;
    
    public CameraSelectView(CameraSelectController cameraSelectController, MaterialsManager materialsManager)
    {
        _defaultMaterial = materialsManager.GetByName(MaterialName.Default);
        _highlightMaterial = materialsManager.GetByName(MaterialName.Highlight);
        _selectMaterial = materialsManager.GetByName(MaterialName.Select);
        _originalMaterials = new Dictionary<GameObject, Material>();
        cameraSelectController.HighlightedChanged += HighlightedChanged;
        cameraSelectController.SelectedChanged += OnSelectedChanged;
    }

    private void HighlightedChanged(GameObject newObject, GameObject oldObject)
    {
        if (newObject)
        {
            AddOriginalMaterial(newObject);
            SetMaterial(newObject, _highlightMaterial);
        }

        if (oldObject)
        {
            ResetMaterial(oldObject);
        }
    }
    
    private void OnSelectedChanged(GameObject newObject, GameObject oldObject)
    {
        if (newObject)
        {
            AddOriginalMaterial(newObject);
            SetMaterial(newObject, _selectMaterial);
        }

        if (oldObject)
        {
            ResetMaterial(oldObject);
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
