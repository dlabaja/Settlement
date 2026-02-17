using Attributes;
using Services;
using UnityEngine;

namespace Components
{
    public class GameStartComponent : MonoBehaviour
    {
        [Autowired] private SettingsService _settingsService;
        
        public void Awake()
        {
            //_settingsManager.SaveSettings();
        }
    }
}
