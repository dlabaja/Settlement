using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Time;

namespace Gui
{
    public class DropdownExt : MonoBehaviour
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        [SerializeField] private float cooldown;
        private float lastClicked;

        private void UpdateDropdown()
        {
            gameObject.GetComponent<Dropdown>().options = gameObjects
                .Select(x => new Dropdown.OptionData(x.ToString())).ToList();
        }

        public GameObject GetChosenElement()
        {
            return gameObjects.ElementAt(GetComponent<Dropdown>().value);
        }

        public void UpdateData(List<GameObject> items)
        {
            gameObjects = items;
            UpdateDropdown();
        }
        
        public void OnFocusClicked()
        {
            if (time - lastClicked < cooldown) return;
            lastClicked = time;

            var obj = GetChosenElement().transform.position;
            var cam = Camera.main!.transform;
            var rotation = cam.rotation;
            var objPos = new Vector3(obj.x,
                obj.y + 5,
                obj.z) + Vector3.back * 3;
            
            StartCoroutine(Utils.SlerpMove(cam, objPos));
            StartCoroutine(Utils.SlerpRotation(cam, 
                Quaternion.Euler(50f, rotation.y, rotation.z)));
        }
    }
}
