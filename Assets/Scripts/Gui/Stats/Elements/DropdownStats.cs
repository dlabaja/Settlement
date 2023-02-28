using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class DropdownStats : MonoBehaviour
    {
        private VisualElement container;
        private VisualElement dropdownContainer;
        private VisualElement dropdown;
        private void Awake()
        {
            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            dropdownContainer = container.Q<VisualElement>("DropdownContainer");
            dropdown = dropdownContainer.Q<DropdownField>();
            var ve = dropdown.Q<VisualElement>();
            ve.style.backgroundColor = new StyleColor(Color.red);
            ve.style.unityBackgroundImageTintColor = new StyleColor(Color.red);
            ve.style.color = new StyleColor(Color.red);

        }
    }
}
