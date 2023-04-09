using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class ButtonElement : MonoBehaviour
    {
        private Button button;
        private void Awake()
        {
            button = GetComponent<UIDocument>().rootVisualElement.Q<Button>();
            button.RegisterCallback<GeometryChangedEvent>(OnLabelResized);
        }

        public void OnLabelResized(GeometryChangedEvent evt)
        {
            var textSize = button.MeasureTextSize(
                button.text,
                0,
                VisualElement.MeasureMode.Undefined,
                button.layout.height,
                VisualElement.MeasureMode.Exactly).x;
            button.style.width = new StyleLength(textSize + 10);
        }
    }
}
