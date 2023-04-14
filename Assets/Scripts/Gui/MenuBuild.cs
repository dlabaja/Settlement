using Buildings;
using Gui.Stats;
using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var button = Instantiate(Resources.Load("UI/MenuBuildButton") as GameObject);
            var buttonRoot = button.GetComponent<UIDocument>().rootVisualElement.Q<Button>();
            root.Q<ScrollView>().Add(buttonRoot);
            if (!string.IsNullOrEmpty(img))
                buttonRoot.style.backgroundImage = new StyleBackground(Utils.LoadTexture(img));

            buttonRoot.schedule.Execute(() => RenderHover(buttonRoot, t, price));
            buttonRoot.Q<Button>().clicked += () =>
            {
                var b = button.GetComponent<MenuBuildButton>();
                var gm = Utils.LoadGameObject(t.Name, Const.Parent.Buildings);

                gm.name = t.Name;
                Stats.Stats.statsEnabled = false;
                ChangeBehaviorsState(gm, false);

                b.isBuilding = true;
                buttonRoot.schedule.Execute(() => b.BuildMode(gm)).Until(() => b.isBuilding == false);
            };
        }

        private static void RenderHover(CallbackEventHandler button, MemberInfo t, BuildingPrice price)
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
        }

        public static void ChangeBehaviorsState(GameObject gm, bool state)
        {
            foreach (var item in gm.GetComponents<MonoBehaviour>())
                item.enabled = state;
            foreach (var item in gm.GetComponents<Behaviour>())
                item.enabled = state;
        }
        
        public void SetToUnbuilt(bool state)
        {
            if (state)
            {
                gameObject.AddComponent<Unbuilt>();
                return;
            }
            Destroy(GetComponent<Unbuilt>());
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
