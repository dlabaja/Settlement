using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gui
{
    public class MenuBuildButton : MonoBehaviour
    {
        public bool isBuilding = true;
        private float rotationY;
        
        public void BuildMode(GameObject gm)
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
                gm.transform.position = new Vector3(hit.point.x, terrain.GetInterpolatedHeight(hit.point.x / terrain.size.x, hit.point.z / terrain.size.z) + collider.bounds.extents.y, hit.point.z);
                gm.transform.rotation = Quaternion.FromToRotation(Vector3.up, terrainNormal);
                gm.transform.Rotate(Vector3.up, rotationY, Space.Self);
            }

            if (Mouse.current.leftButton.isPressed && renderer.material.color == Color.white)
                EndBuildMode();
            else if (Mouse.current.rightButton.isPressed || Keyboard.current.escapeKey.isPressed)
            {
                EndBuildMode();
                Destroy(gm);
            }
            else if (Keyboard.current.rKey.isPressed)
                rotationY += 0.5f;
        }
        
        private void EndBuildMode()
        {
            isBuilding = false;
            Stats.Stats.statsEnabled = true;
        }
    }
}
