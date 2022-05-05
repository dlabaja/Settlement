using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IInventoryPickable
    {
        public void PickItems(GameObject gameObject, GameObject gm, List<Const.Items> items)
        {
            var customObject = gameObject.GetComponent<CustomObject>();
            foreach (var item in items) customObject.GetAllItems(gm, item);
        }
    }
}