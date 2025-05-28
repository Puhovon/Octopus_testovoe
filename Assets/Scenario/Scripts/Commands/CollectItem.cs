using Naninovel;
using Scenario.Scripts.Quest;

namespace Scenario.Scripts
{
    [CommandAlias("collectItem")]
    public class CollectItem : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var questSystem = Engine.GetService<QuestSystem>();
            questSystem.CollectItem();
            return UniTask.CompletedTask;
        }
    }
}