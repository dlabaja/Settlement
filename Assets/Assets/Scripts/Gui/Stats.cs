using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Gui
{
    public class Stats : MonoBehaviour
    {
        public static void DrawStats(GameObject gm)
        {
            if (gm.GetComponent<CustomObject>() == null) return;
            if (gm.GetComponent<Entity>() != null)
            {
                DrawEntityStats(gm.GetComponent<Entity>());
            }
        }

        async private static void DrawEntityStats(Entity entity)
        {
            var ui = Utils.LoadGameObject("StatsEntity", Const.Parent.Canvas);
            print(ui);
            await Task.Delay(2000);
            ui.GetComponent<RectTransform>().localScale = new Vector2(0.3f, 0.6f);
        }
    }
}
