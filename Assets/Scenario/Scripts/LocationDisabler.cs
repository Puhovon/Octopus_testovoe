using UnityEngine;
using Naninovel;

public class LocationDisabler : MonoBehaviour
{
    public string locationName;
    
    public void DisableLocation()
    {
        // Здесь логика отключения локации
        Debug.Log($"Location disabled: {locationName}");
        GetComponent<Collider2D>().enabled = false;
        
        // Возвращаемся в наниновель
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.PreloadAndPlayAsync("NextScriptAfterItem").Forget();
    }
}