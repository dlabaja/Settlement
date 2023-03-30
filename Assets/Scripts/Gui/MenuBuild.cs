using Gui.Stats;
using Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Gui
{
    public class MenuBuild : MonoBehaviour
    {
        private static VisualElement root;
        public static bool isBuilding;

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

            button.schedule.Execute(() => RenderHover(button, t, price));
            button.Q<Button>().clicked += () =>
            {
                var gm = Utils.LoadGameObject(t.Name, Const.Parent.Buildings);
                isBuilding = true;
                Stats.Stats.statsEnabled = false;
                button.schedule.Execute(() => BuildMode(gm)).Until(() => isBuilding == false);
            };
        }

        private static void BuildMode(GameObject gm)
        {
            var collider = gm.GetComponent<Collider>();
            var renderer = gm.GetComponent<Renderer>();
            Ray ray = Camera.main!.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << LayerMask.NameToLayer("Terrain")))
            {
                if (Physics.OverlapSphere(collider.bounds.center, collider.bounds.extents.magnitude - 1)
                    .Any(x => x.gameObject.layer == LayerMask.NameToLayer("Default") && x.gameObject != gm))
                    renderer.material.color = Color.red;
                else
                    renderer.material.color = Color.white;

                var terrain = Terrain.activeTerrain.terrainData;
                var terrainNormal = terrain.GetInterpolatedNormal(hit.point.x / terrain.size.x, hit.point.z / terrain.size.z);
                gm.transform.position = new Vector3(hit.point.x, terrain.GetInterpolatedHeight(hit.point.x / terrain.size.x, hit.point.z / terrain.size.z) + 1, hit.point.z);
                gm.transform.rotation = Quaternion.FromToRotation(Vector3.up, terrainNormal);
            }

            if (Mouse.current.leftButton.isPressed && renderer.material.color == Color.white)
                EndBuildMode();
            else if (Mouse.current.rightButton.isPressed || Keyboard.current.escapeKey.isPressed)
            {
                EndBuildMode();
                Destroy(gm);
            }

        }

        private static void EndBuildMode()
        {
            isBuilding = false;
            Stats.Stats.statsEnabled = true;
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
