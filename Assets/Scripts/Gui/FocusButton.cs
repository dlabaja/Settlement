using UnityEngine;

namespace Gui
{
    public class FocusButton : DropdownExt
    {
        public void OnFocusClicked()
        {
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
