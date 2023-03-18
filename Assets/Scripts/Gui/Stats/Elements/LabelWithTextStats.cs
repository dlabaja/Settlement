using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class LabelWithTextStats : MonoBehaviour
    {
        private Label label;
        private Label text;
        private VisualElement container;

        private void Awake()
        {
            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            label = container.Q<Label>();
            text = container.Q<Label>("Text");

            label.RegisterCallback<GeometryChangedEvent>(OnLabelResized);
            text.RegisterCallback<GeometryChangedEvent>(OnLabelResized);
        }
        
        private void OnLabelResized(GeometryChangedEvent e)
        {
            var textSize = text.MeasureTextSize(
                text.text,
                0,
                VisualElement.MeasureMode.Undefined,
                text.layout.height,
                VisualElement.MeasureMode.Exactly).x;
            var labelSize = label.MeasureTextSize(
                label.text,
                0,
                VisualElement.MeasureMode.Undefined,
                label.layout.height,
                VisualElement.MeasureMode.Exactly).x;
            container.style.width = new StyleLength(textSize + labelSize + 15);
        }
    }
}
