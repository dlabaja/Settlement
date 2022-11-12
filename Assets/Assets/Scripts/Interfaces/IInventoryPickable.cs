using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IInventoryPickable
    {
        public void PickItems(GameObject gameObject, GameObject gm, List<Const.Item> items)
        {
            var customObject = gameObject.GetComponent<Inventory>();
            foreach (var item in items) customObject.TransferItems(gm, item, customObject.GetItemCount(item));
        }
    }
}