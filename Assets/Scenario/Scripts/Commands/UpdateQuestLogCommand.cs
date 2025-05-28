using Naninovel;
using Naninovel.Commands;
using Scenario.Scripts.Quest;
using UnityEngine;

[CommandAlias("updateQuest")]
public class UpdateQuestLogCommand :Command
{
    public StringParameter title;
    public StringParameter description;
    public IntegerParameter step;
    
    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        
        var res = Engine.TryGetService<QuestSystem>(out QuestSystem questSystem);
        if(!res)
        {
            Debug.Log("QuestSystem not found");
            return;
        }
        questSystem.UpdateQuest(
            title ?? string.Empty,
            description ?? string.Empty,
            step ?? 0
        );
    }
}