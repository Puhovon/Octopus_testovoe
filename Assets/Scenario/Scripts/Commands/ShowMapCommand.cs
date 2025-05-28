using Naninovel;
using UnityEngine;

namespace Scenario.Scripts
{
    [CommandAlias("showMap")]
    public class ShowMapCommand :Command
    {
        [ParameterAlias("reset")]
        public BooleanParameter ResetSelection = false;
        
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            
            var mapUI = uiManager.GetUI<MapUI>();
            if (mapUI == null)
            {
                Debug.LogError("MapUI not found in UI manager");
                return;
            }

            var selectedLocation = await mapUI.ShowMapAsync();
            
            if (!string.IsNullOrEmpty(selectedLocation))
            {
                var variableManager = Engine.GetService<ICustomVariableManager>();
                variableManager.SetVariableValue("selectedLocation", selectedLocation);
                
                Debug.Log($"Selected location: {selectedLocation}");
            }
            else
            {
                Debug.Log("Map closed without selection");
            }
        }
    }
}