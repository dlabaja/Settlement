using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Components
{
    public class GameStartComponent : MonoBehaviour
    {
        [Inject] private SettingsService _settingsService;
        
        public void Awake()
        {
            //_settingsManager.SaveSettings();
        }
    }
}
