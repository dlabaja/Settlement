using Gui.Stats;
using Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui
{
    public class MenuBuild : MonoBehaviour
    {
        private static VisualElement root;

        private void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            var scroller = root.Query<Scroller>().Last();
            scroller.valueChanged += f => scroller.value = Math.Clamp(f, 0, Math.Abs(scroller.highValue));
            ToggleOpen();
        }

        public static void ToggleOpen() => root.visible = !root.visible;

        public static void AddButton(Type t, BuildingPrice price, string img)
        {
            var button = ((VisualTreeAsset)Resources.Load("UI Toolkit/MenuButton")).Instantiate().contentContainer;
            root.Q<ScrollView>().Add(button);
            if (!string.IsNullOrEmpty(img))
                button.style.backgroundImage = new StyleBackground(Utils.LoadTexture(img));

            //tooltip on hover
            button.schedule.Execute(() =>
            {
                var gm = Window.GenerateWindow(new GameObject())
                    .AddLabel(t.Name)
                    .AddLabelWithText("Materials:", Utils.ListToString(price.materials))
                    .AddLabel(() => $"Golds: {price.golds}"); 
                gm.BuildWindow();
                gm.GetComponent<UIDocument>().sortingOrder = (float)Math.Pow(2, 128);

                var top = gm.GetComponent<UIDocument>().rootVisualElement.Q("Top");
                top.visible = false;
                button.RegisterCallback<PointerMoveEvent>(_ =>
                {
                    top.visible = true;
                    var mousePos = Mouse.current.position.ReadValue();
                    top.style.top = Screen.height - mousePos.y - top.style.height.value.value - 20;
                    top.style.left = mousePos.x - (0.5f * top.style.width.value.value);
                });
                button.RegisterCallback(delegate(PointerLeaveEvent _)
                {
                    top.visible = false;
                });
            });
        }
    }

    [Serializable]
    public struct BuildingPrice
    {
        public BuildingPrice(List<ItemStruct> materials, int golds)
        {
            this.materials = materials;
            this.golds = golds;
        }

        public List<ItemStruct> materials;
        public int golds;

        public override string ToString() => $"({Utils.ListToString(materials)}, Gold: {golds})";
    }
}
