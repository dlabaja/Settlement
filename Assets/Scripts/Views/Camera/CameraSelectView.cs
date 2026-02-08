using Constants;
using JetBrains.Annotations;
using Managers;
using Models.Controllers.Camera;
using System.Collections.Generic;
using UnityEngine;

namespace Views.Camera;

public class CameraSelectView
{
    private readonly CameraSelectController _cameraSelectController;
    private readonly Dictionary<Renderer, Material> _originalMaterials;
    private readonly Material _defaultMaterial;
    private readonly Material _highlightMaterial;
    private readonly Material _selectMaterial;
    private readonly LayerMask _selectableLayerMask;
    
    public CameraSelectView(CameraSelectController cameraSelectController, MaterialsManager materialsManager)
    {
        _defaultMaterial = materialsManager.GetByName(MaterialName.Default);
        _highlightMaterial = materialsManager.GetByName(MaterialName.Highlight);
        _selectMaterial = materialsManager.GetByName(MaterialName.Select);
        _originalMaterials = new Dictionary<Renderer, Material>();
        _cameraSelectController = cameraSelectController;
        _selectableLayerMask = LayerMask.GetMask(PhysicsLayer.Selectable);
        cameraSelectController.HighlightedChanged += HighlightedChanged;
        cameraSelectController.SelectedChanged += OnSelectedChanged;
    }

    public void Dispose()
    {
        _cameraSelectController.HighlightedChanged -= HighlightedChanged;
        _cameraSelectController.SelectedChanged -= OnSelectedChanged;
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

    private void HighlightedChanged([CanBeNull] GameObject newObject, [CanBeNull] GameObject oldObject)
    {
        if (newObject)
        {
            var newRenderer = newObject.GetComponent<Renderer>();
            AddOriginalMaterial(newRenderer);
            if (newObject != _cameraSelectController.Selected)
            {
                SetMaterial(newRenderer, _highlightMaterial);
            }
        }

        if (oldObject && oldObject != _cameraSelectController.Selected)
        {
            var oldRenderer = oldObject.GetComponent<Renderer>();
            ResetMaterial(oldRenderer);
        }
    }
    
    private void OnSelectedChanged([CanBeNull] GameObject newObject, [CanBeNull] GameObject oldObject)
    {
        if (newObject)
        {
            var newRenderer = newObject.GetComponent<Renderer>();
            AddOriginalMaterial(newRenderer);
            SetMaterial(newRenderer, _selectMaterial);
        }

        if (oldObject)
        {
            var oldRenderer = oldObject.GetComponent<Renderer>();
            ResetMaterial(oldRenderer);
        }
    }

    private void AddOriginalMaterial(Renderer renderer)
    {
        _originalMaterials.TryAdd(renderer, renderer.sharedMaterial);
    }

    private Material GetOriginalMaterial(Renderer renderer)
    {
        var value = _originalMaterials.TryGetValue(renderer, out var material);
        return value ? material : _defaultMaterial;
    }

    private void SetMaterial(Renderer renderer, Material material)
    {
        renderer.sharedMaterial = material;
    }
    
    private void ResetMaterial(Renderer renderer)
    {
        renderer.sharedMaterial = GetOriginalMaterial(renderer);
        _originalMaterials.Remove(renderer);
    }
}
