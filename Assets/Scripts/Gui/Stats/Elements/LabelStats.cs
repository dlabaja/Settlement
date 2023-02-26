using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class LabelStats : MonoBehaviour
    {
        private Label label;
        private void Awake()
        {
            label = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").Q<Label>();
            label.RegisterCallback<GeometryChangedEvent>(OnLabelResized);
        }

        public void SetLabel(string text)
        {
            label.text = text;
        }

        private void OnLabelResized(GeometryChangedEvent e)
        {
            var textSize = label.MeasureTextSize(
                label.text, 0, VisualElement.MeasureMode.Undefined,
                label.layout.height, VisualElement.MeasureMode.Exactly).x;
            label.style.width = new StyleLength(textSize + 5);
        }
    }
}
