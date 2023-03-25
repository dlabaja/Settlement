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
    public class AssignDropdownElement : DropdownElement
    {
        private void Awake()
        {
            afterAwake = () =>
            {
                var workplace = sender.GetComponent<Workplace>();
                items = workplace.GetWorkers().Select(x => x.gameObject).ToList();
                OnItemsChanged();

                workplace.OnWorkersChanged += () =>
                {
                    items = workplace.GetWorkers().Select(x => x.gameObject).ToList();
                    SetInnerLabel($"Workers: {items.Count}/{workplace.GetMaxWorkers()}");
                    OnItemsChanged();
                    ReloadDropdownItems();
                };

                buttonClicked = delegate
                {
                    var worker = FindObjectsOfType<Entity>()
                        .FirstOrDefault(x => x.Workplace.name == Const.CustomObjects.Spawn.ToString());
                    sender.GetComponent<Workplace>().AssignWorker(worker);
                };


                itemButtonClicked = delegate(GameObject gm)
                {
                    sender.GetComponent<Workplace>().FireWorker(gm.GetComponent<Entity>());
                };

                SetOuterLabel("Workers");
                SetInnerLabel($"Workers: {items.Count}/{workplace.GetMaxWorkers()}");
                SetDropdownButtonImage("Assets/Resources/Sprites/assign.png");
                SetDropdownItemButtonImage("Assets/Resources/Sprites/unassign.png");
            };
        }
    }
}
