using Buildings.Workplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gui.Stats.Elements
{
    public class AssignDropdownStats : DropdownStats
    {
        private void Start()
        {
            SetDropdownButtonImage("Assets/Sprites/assign.png");
            //SetDropdownItemButtonImage("Assets/Sprites/unassign.png");
        }

        public void SetupSender(GameObject senderObj)
        {
            sender = senderObj;
            var workplace = sender.GetComponent<Workplace>();
            items = workplace.GetWorkers();
            OnItemsChanged();

            workplace.OnWorkersChanged += () =>
            {
                items = workplace.GetWorkers();
                SetInnerLabel($"Workers: {items.Count}/{workplace.GetMaxWorkers()}");
                OnItemsChanged();
                ReloadDropdownItems();
            };

            container.Q<VisualElement>("ButtonContainer").Q<Button>().clicked += () =>
            {
                var worker = FindObjectsOfType<Entity>()
                    .FirstOrDefault(x => x.Workplace.name == Const.CustomObjects.Spawn.ToString());
                workplace.AssignWorker(worker);
            };
            SetOuterLabel("Workers");
            SetInnerLabel($"Workers: {items.Count}/{workplace.GetMaxWorkers()}");
        }

        private void OpenDropdown()
        {
            //

            //s


        }
    }
}
