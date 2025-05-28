using Naninovel;
using UnityEngine;

namespace Scenario.Scripts
{
    [CommandAlias("startMemoryGame")]
    public class StartMemoryGame : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var input = Engine.GetService<IInputManager>();
            input.ProcessInput = false;

            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            scriptPlayer.Stop();

            var memoryGamePrefab = Resources.Load<GameObject>("MemoryGame");
            var instance = GameObject.Instantiate(memoryGamePrefab);

            var memoryGame = instance.GetComponent<MemoryGameController>();
            bool success = await memoryGame.PlayGameAsync();
            Debug.Log("Game end with result " + success );
            GameObject.Destroy(instance);
            Debug.Log(instance is null);
            scriptPlayer.Play();
            input.ProcessInput = true;
            Debug.Log($"{scriptPlayer.HasPlayed("Location2")}, {input.ProcessInput}");

            var variableManager = Engine.GetService<ICustomVariableManager>();
            variableManager.SetVariableValue("MemoryGameResult", success.ToString());

        }
    }
}