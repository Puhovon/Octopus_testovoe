using UnityEngine;
using Naninovel;

public class LocationDisabler : MonoBehaviour
{
    public string locationName;
    
    public void DisableLocation()
    {
        Debug.Log($"Location disabled: {locationName}");
        GetComponent<Collider2D>().enabled = false;
        
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.PreloadAndPlayAsync("NextScriptAfterItem").Forget();
    }
}