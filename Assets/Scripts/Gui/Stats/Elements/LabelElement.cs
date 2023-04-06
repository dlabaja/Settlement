using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class LabelElement : MonoBehaviour
    {
        private Label label;
        private VisualElement container;
        private void Awake()
        {
            container = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            label = container.Q<Label>();
            label.RegisterCallback<GeometryChangedEvent>(OnLabelResized);
        }

        public void OnLabelResized(GeometryChangedEvent e)
        {
            var textSize = label.MeasureTextSize(
                label.text, 0, VisualElement.MeasureMode.Undefined,
                label.layout.height, VisualElement.MeasureMode.Exactly).x;
            container.style.width = new StyleLength(textSize);
        }
    }
}
