using Attributes;
using Managers;
using UnityEngine;

namespace Components
{
    public class GameStartComponent : MonoBehaviour
    {
        [Autowired] private SettingsManager _settingsManager;
        
        public void Awake()
        {
            //_settingsManager.SaveSettings();
        }
    }
}
