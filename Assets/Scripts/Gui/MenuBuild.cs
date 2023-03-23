using Buildings.Workplace;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui
{
    public class MenuBuild : MonoBehaviour
    {
        private static Dictionary<Type, string> buildableObjects = new Dictionary<Type, string>{
            {typeof(Spawn), ""},
        };
        private static VisualElement root;

        private void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            var scroller = root.Query<Scroller>().Last();
            scroller.valueChanged += f => scroller.value = Math.Clamp(f, 0, Math.Abs(scroller.highValue));
            ToggleOpen();
        }

        public static void ToggleOpen() => root.visible = !root.visible;

        public static void AddButton(Type t, string img)
        {
            var button = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI Toolkit/MenuButton.uxml").Instantiate().contentContainer;
            root.Q<ScrollView>().Add(button);
            buildableObjects.Add(t, img);
            if (!string.IsNullOrEmpty(img))
                button.style.backgroundImage = new StyleBackground(Utils.LoadTexture(img));
        }
    }
}
