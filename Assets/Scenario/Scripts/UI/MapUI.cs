using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scenario.Scripts
{
    public class MapUI : CustomUI
    {
        [Header("Map Settings")]
        [SerializeField] private GameObject mapPanel;
        [SerializeField] private Button location1Button;
        [SerializeField] private Button location2Button;
        [SerializeField] private Button location3Button;
        [SerializeField] private Button closeButton;
        
        [Tooltip("Should map pause Naninovel input?")]
        [SerializeField] private bool blockInput = true;
        
        private UniTaskCompletionSource<string> selectionSource;
        private IInputManager inputManager;

        protected override void Awake()
        {
            base.Awake();
            inputManager = Engine.GetService<IInputManager>();
            
            location1Button.onClick.AddListener(() => SelectLocation("ForestVillage"));
            location2Button.onClick.AddListener(() => SelectLocation("ForgottenForest"));
            location3Button.onClick.AddListener(() => SelectLocation("AncientTemple"));
            closeButton.onClick.AddListener(CloseMap);
            
            mapPanel.SetActive(false);
        }

        public UniTask<string> ShowMapAsync()
        {
            selectionSource = new UniTaskCompletionSource<string>();
            print(mapPanel);
            mapPanel.SetActive(true);
            Visible = true;
            
            if (blockInput) inputManager.ProcessInput = false;
            
            return selectionSource.Task;
        }

        private void SelectLocation(string locationId)
        {
            Debug.Log($"Location selected: {locationId}");
            selectionSource?.TrySetResult(locationId);
            CloseMap();
        }

        private void CloseMap()
        {
            selectionSource?.TrySetResult(null);
            mapPanel.SetActive(false);
            Visible = false;
            
            // Восстанавливаем ввод
            if (blockInput) inputManager.ProcessInput = true;
        }
    }
}