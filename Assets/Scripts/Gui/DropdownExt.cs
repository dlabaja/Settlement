using Buildings.Workplace;
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
        public GameObject sender;

        private void UpdateDropdown(string firstItem)
        {
            var data = new List<Dropdown.OptionData>();
            data.AddRange(gameObjects
                .Select(x => new Dropdown.OptionData(x.ToString()))
                .Prepend(new Dropdown.OptionData(firstItem))
                .ToList());
            gameObject.GetComponent<Dropdown>().options = data;
        }

        public GameObject GetChosenElement()
        {
            return gameObjects.ElementAt(GetComponent<Dropdown>().value);
        }

        public void UpdateData(List<GameObject> items, string firstItem = null)
        {
            gameObjects = items;
            UpdateDropdown(firstItem);
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

        public void OnAssignClicked()
        {
            sender.GetComponent<Workplace>().AssignWorker(
                FindObjectsOfType<Entity>().FirstOrDefault(x => x.Workplace.name == Const.CustomObjects.Spawn.ToString())?.gameObject);
        }

        public void OnUnassignClicked()
        {
            sender.GetComponent<Workplace>().FireWorker(GetChosenElement());
        }
    }
}
