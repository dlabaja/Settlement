using JetBrains.Annotations;
using UnityEngine;

namespace Models.Camera;

public delegate void ObjectChangedNullable<in T>([CanBeNull] T newObject, [CanBeNull] T oldObject);

public class CameraSelect
{
    public GameObject LastHighlighted;
    public GameObject LastSelected;
    public GameObject Highlighted;
    public GameObject Selected;
    public event ObjectChangedNullable<GameObject> HighlightedChanged;
    public event ObjectChangedNullable<GameObject> SelectedChanged;
    
    public void Select(GameObject gameObject)
    {
        if (gameObject == Selected)
        {
            return;
        }

        LastSelected = Selected;
        Selected = gameObject;
        SelectedChanged?.Invoke(Selected, LastSelected);
    }
    
    public void ResetSelect()
    {
        if (!Selected)
        {
            return;
        }

        LastSelected = Selected;
        Selected = null;
        SelectedChanged?.Invoke(Selected, LastSelected);
    }
    
    public void Highlight(GameObject gameObject)
    {
        if (gameObject == Highlighted)
        {
            return;
        }
        
        LastHighlighted = Highlighted;
        Highlighted = gameObject;
        HighlightedChanged?.Invoke(Highlighted, LastHighlighted);
    }

    public void ResetHighlight()
    {
        if (!Highlighted)
        {
            return;
        }

        LastHighlighted = Highlighted;
        Highlighted = null;
        HighlightedChanged?.Invoke(Highlighted, LastHighlighted);
    }
}
