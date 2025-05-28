using Naninovel;
using Naninovel.UI;
using UnityEngine;
using TMPro ;
public class QuestLogUI : CustomUI
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject panel;
    
    private ICustomVariableManager variableManager;

    protected override void Awake ()
    {
        base.Awake();
        variableManager = Engine.GetService<ICustomVariableManager>();
        panel.SetActive(false);
    }

    public void UpdateContent ()
    {
        if (variableManager.TryGetVariableValue<string>("questTitle", out var title) &&
            variableManager.TryGetVariableValue<string>("questDescription", out var description))
        {
            titleText.text = title;
            descriptionText.text = description;
            
            panel.SetActive(!string.IsNullOrEmpty(title));
        }
    }

    public void ToggleVisibility () => panel.SetActive(!panel.activeSelf);
}