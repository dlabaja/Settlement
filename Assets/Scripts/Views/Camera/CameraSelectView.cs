using Constants;
using Managers;
using Models.Controllers.Camera;
using System.Collections.Generic;
using UnityEngine;

namespace Views.Camera;

public class CameraSelectView
{
    private readonly CameraSelectController _cameraSelectController;
    private readonly Dictionary<GameObject, Material> _originalMaterials;
    private readonly Material _defaultMaterial;
    private readonly Material _highlightMaterial;
    private readonly Material _selectMaterial;
    private readonly LayerMask _selectableLayerMask;
    
    public CameraSelectView(CameraSelectController cameraSelectController, MaterialsManager materialsManager)
    {
        _defaultMaterial = materialsManager.GetByName(MaterialName.Default);
        _highlightMaterial = materialsManager.GetByName(MaterialName.Highlight);
        _selectMaterial = materialsManager.GetByName(MaterialName.Select);
        _originalMaterials = new Dictionary<GameObject, Material>();
        _cameraSelectController = cameraSelectController;
        cameraSelectController.HighlightedChanged += HighlightedChanged;
        cameraSelectController.SelectedChanged += OnSelectedChanged;
        _selectableLayerMask = LayerMask.GetMask(PhysicsLayer.Selectable);
    }

    public void Process(CameraRayController cameraRayController, Vector3 mousePosition, bool selectPressed)
    {
        var hit = cameraRayController.TryRaycast(mousePosition, out var raycastedObj, _selectableLayerMask);
        if (hit)
        {
            ProcessHit(raycastedObj.transform.gameObject, selectPressed);
            return;
        }
        ProcessMiss(selectPressed);
    }

    private void ProcessHit(GameObject hitObj, bool selectPressed)
    {
        if (selectPressed)
        {
            _cameraSelectController.Select(hitObj);
        }
        _cameraSelectController.Highlight(hitObj);
    }

    private void ProcessMiss(bool selectPressed)
    {
        if (selectPressed)
        {
            _cameraSelectController.ResetSelect();
        }
        _cameraSelectController.ResetHighlight();
    }

    private void HighlightedChanged(GameObject newObject, GameObject oldObject)
    {
        if (newObject)
        {
            AddOriginalMaterial(newObject);
        }

        if (newObject && newObject != _cameraSelectController.Selected)
        {
            SetMaterial(newObject, _highlightMaterial);
        }

        if (oldObject && oldObject != _cameraSelectController.Selected)
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
