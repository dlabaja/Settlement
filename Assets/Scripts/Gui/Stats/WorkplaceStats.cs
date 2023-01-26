using Buildings.Workplace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Gui.Stats
{
    public class WorkplaceStats : Stats
    {
        public void DrawWorkplaceStats(Workplace workplace)
        {
            //producing -> planks for wood
            var ui = gameObject.GetComponent<RectTransform>();
            var child = ui.transform.Find("Image");
            var name = child.Find("Name").GetComponent<Text>();
            var workers = child.Find("Workers").GetComponent<AssignDropdown>();
            var producing = child.Find("Producing").GetComponent<Text>();
            var inventory = child.Find("Inventory").GetComponent<Text>();

            name.text = workplace.name;
            workers.sender = workplace.gameObject;
            producing.text = "Nic lol";

            StartCoroutine(UpdateData(workplace, workers, inventory));
        }

        private IEnumerator UpdateData(Workplace workplace, AssignDropdown workers, Text inventory)
        {
            while (true)
            {
                //todo house
                workers.UpdateData(
                    workplace.GetWorkers(),
                    $"Workers: {workplace.GetWorkers().Count}/{workplace.GetMaxWorkers()}"
                );

                inventory.text = Utils.DictToString(workplace.GetComponent<Inventory.Inventory>().GetInventory());
                if (string.IsNullOrEmpty(inventory.text)) inventory.text = "---";

                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
