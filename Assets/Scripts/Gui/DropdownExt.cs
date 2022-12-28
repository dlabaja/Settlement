using Assets.Scripts.Buildings.Workplace;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DropdownExt : MonoBehaviour
    {
        //public List<GameObject> items;

        public void UpdateData(List<GameObject> items)
        {
            var dropdown = gameObject.GetComponent<Dropdown>();
            var ext = gameObject.GetComponent<DropdownExt>();
            dropdown.options.Clear();
            //ext.items.Add(entity.Workplace);
            foreach (var item in items)
            {
                dropdown.options.Add(new Dropdown.OptionData(item.name));
            }
        }

        public void OnFocusClicked()
        {
            var obj = GameObject.Find(gameObject.GetComponent<Dropdown>().options[0].text);
            print(gameObject.GetComponent<Dropdown>().options[0].text);
            Camera.main!.transform.position = obj.transform.position;
        }
    }
}
