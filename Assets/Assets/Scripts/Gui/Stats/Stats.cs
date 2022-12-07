using UnityEngine;

namespace Assets.Scripts.Gui.Stats
{
    public class Stats : MonoBehaviour
    {
        public static void DrawStats(GameObject gm)
        {
            if (gm.GetComponent<CustomObject>() == null) return;
            if (gm.GetComponent<Entity>() != null)
            {
                Utils.LoadGameObject("StatsEntity", Const.Parent.Canvas).GetComponent<RectTransform>();
            }
        }
    }
}
