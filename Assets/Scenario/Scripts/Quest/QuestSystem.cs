using Naninovel;

namespace Scenario.Scripts.Quest
{
    public class QuestSystem : IEngineService
    {
        private const string TitleVariable = "questTitle";
        private const string DescVariable = "questDescription";
        private const string StepVariable = "questStep";
    
        private readonly ICustomVariableManager variableManager;
        private readonly IStateManager stateManager;
        private QuestLogUI questLogUI;
        
        public UniTask InitializeServiceAsync()
        {
            var uiManager = Engine.GetService<IUIManager>();
            questLogUI = uiManager.GetUI<QuestLogUI>();
            return UniTask.CompletedTask;
        }

        public QuestSystem (ICustomVariableManager variableManager, IStateManager stateManager)
        {
            this.variableManager = variableManager;
            this.stateManager = stateManager;
        }

        public void InitializeService () 
        {
            var uiManager = Engine.GetService<IUIManager>();
            questLogUI = uiManager.GetUI<QuestLogUI>();
        }

        public void UpdateQuest (string title, string description, int step)
        {
            variableManager.SetVariableValue(TitleVariable, title);
            variableManager.SetVariableValue(DescVariable, description);
            variableManager.SetVariableValue(StepVariable, step.ToString());
        
            stateManager.SaveGlobalAsync().Forget();
            questLogUI.UpdateContent();
        }

        public void CollectItem ()
        {
            variableManager.SetVariableValue("ItemCollected", "true");
            stateManager.SaveGlobalAsync().Forget();
        }

        public void ResetService () {}
        public void DestroyService () {}
    }
}