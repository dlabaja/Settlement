using Buildings.Workplace;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Time;

namespace Gui
{
    public class FocusDropdown : DropdownExt
    {
        [SerializeField] private float cooldown = 0.5f;
        private float lastClicked;

        public void OnFocusClicked()
        {
            if (time - lastClicked < cooldown && lastClicked != 0) return;
            lastClicked = time;

            var obj = GetChosenElement().transform.position;
            var cam = Camera.main!.transform;
            var rotation = cam.rotation;
            var objPos = new Vector3(obj.x,
                obj.y + 5,
                obj.z) + Vector3.back * 3;
            
            KeystrokesController.KeystrokesController.DisableInput();
            StartCoroutine(Utils.SlerpMove(cam, objPos,
                KeystrokesController.KeystrokesController.EnableInput));
            StartCoroutine(Utils.SlerpRotation(cam, Quaternion.Euler(50f, rotation.y, rotation.z)));
        }
    }
}
