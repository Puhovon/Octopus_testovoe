using Naninovel;
using Naninovel.UI;
using UnityEngine;

[CommandAlias("startMemoryGame")]
public class StartMemoryGameCommand : Command
{
    
    public override async UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        
        var gamePrefab = Resources.Load<GameObject>("MemoryGame");
        var instance = Object.Instantiate(gamePrefab);
        
        var gameController = instance.GetComponent<MemoryGameController>();
        gameController.StartGame();
        
        // 4. Ждать завершения
        var waitCompletion = new UniTaskCompletionSource();
        gameController.OnGameCompleted += () => waitCompletion.TrySetResult();
        await waitCompletion.Task;
        
        // 5. Очистка
        Object.Destroy(instance);
    }
}