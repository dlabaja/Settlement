using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Time;

namespace Gui.Stats.Elements
{
    public class FocusDropdownElement : DropdownElement
    {
        [SerializeField] private float cooldown = 0.2f;
        private float lastClicked;

        public void SetupDropdown(List<GameObject> items, string outerLabel)
        {
            afterAwake = delegate
            {
                listObjects = items;
                buttonClicked = () => FocusItem(items.FirstOrDefault());
                itemButtonClicked = FocusItem;
                onChoose = _ =>
                {
                    SetInnerLabel(chosenItem.name);
                    listObjects = items.Where(x => x != chosenItem).ToList();
                    sender.GetComponent<Entity>().Workplace = GetChosenItem();
                };
                listObjects = items.Skip(1).ToList();
                //todo update itemů

                SetOuterLabel(outerLabel);
                SetInnerLabel(items.FirstOrDefault()?.name);
                SetDropdownButtonImage("Assets/Resources/Sprites/focus.png");
                SetDropdownItemButtonImage("Assets/Resources/Sprites/focus.png");
            };
        }

        private void FocusItem(GameObject gm)
        {
            if (time - lastClicked < cooldown && lastClicked != 0)
                return;
            lastClicked = time;

            var obj = gm.transform.position;
            var cam = Camera.main!.transform;
            var rotation = cam.rotation;
            var objPos = new Vector3(obj.x,
                obj.y + 5,
                obj.z) + Vector3.back * 3;

            KeystrokesController.KeystrokesController.DisableInput();
            StartCoroutine(Utils.SlerpMove(cam,
                objPos,
                KeystrokesController.KeystrokesController.EnableInput));
            StartCoroutine(Utils.SlerpRotation(cam, Quaternion.Euler(50f, rotation.y, rotation.z)));
        }
    }
}
