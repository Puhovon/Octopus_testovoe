using System;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scenario.Scripts
{
    public class MapUI : CustomUI
    {
        [SerializeField] private Button location1Button;
        [SerializeField] private Button location2Button;
        [SerializeField] private Button location3Button;
        [SerializeField] private GameObject panel;
    
        private IScriptPlayer scriptPlayer;

        protected override void Awake ()
        {
            base.Awake();
            scriptPlayer = Engine.GetService<IScriptPlayer>();
            panel.SetActive(false);
        
            location1Button.onClick.AddListener(() => TravelToLocation("Location1"));
            location2Button.onClick.AddListener(() => TravelToLocation("Location2"));
            location3Button.onClick.AddListener(() => TravelToLocation("Location3"));
        }

        private void TravelToLocation (string location)
        {
            panel.SetActive(false);
            scriptPlayer.PreloadAndPlayAsync(location).Forget();
        }

        public void ToggleVisibility ()
        {
            panel.SetActive(!panel.activeSelf);
            // Обновляем доступность локаций
            var variableManager = Engine.GetService<ICustomVariableManager>();
            bool itemCollected = Convert.ToBoolean(variableManager.GetVariableValue("ItemCollected"));
            location3Button.interactable = itemCollected;
        }
    }
}