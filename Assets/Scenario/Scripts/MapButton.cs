using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class MapButton : MonoBehaviour
{
    public Button mapButton;
    public GameObject mapPanel;
    private bool mapVisible;

    private void Start()
    {
        mapButton.onClick.AddListener(ToggleMap);
        mapPanel.SetActive(false);
    }

    public void ToggleMap()
    {
        mapVisible = !mapVisible;
        mapPanel.SetActive(mapVisible);
        
        var stateManager = Engine.GetService<IStateManager>();
        
        if (mapVisible)
        {
            stateManager.PushRollbackSnapshot(); 
            stateManager.ResetStateAsync(); 
        }
        else
        {
            stateManager.PushRollbackSnapshot();
        }
    }
}