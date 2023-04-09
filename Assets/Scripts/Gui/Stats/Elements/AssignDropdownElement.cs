using Buildings.Workplace;
using System.Linq;
using UnityEngine;

namespace Gui.Stats.Elements
{
    public class AssignDropdownElement : DropdownElement
    {
        private void Awake()
        {
            afterAwake = () =>
            {
                var workplace = sender.GetComponent<Workplace>();
                listObjects = workplace.GetWorkers().Select(x => x.gameObject).ToList();
                OnItemsChanged();

                workplace.OnWorkersChanged += () =>
                {
                    listObjects = workplace.GetWorkers().Select(x => x.gameObject).ToList();
                    SetInnerLabel($"Workers: {listObjects.Count}/{workplace.GetMaxWorkers()}");
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
                SetInnerLabel($"Workers: {listObjects.Count}/{workplace.GetMaxWorkers()}");
                SetDropdownButtonImage("Assets/Resources/Sprites/assign.png");
                SetDropdownItemButtonImage("Assets/Resources/Sprites/unassign.png");
            };
        }
    }
}
