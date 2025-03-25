using UnityEngine;

public class MapScript : MonoBehaviour
{

    [SerializeField]
    private Canvas map;
    void Start()
    {
        GameEventSystem.AddListener(OnGameStateChangedEvent, nameof(GameState));
        OnGameStateChangedEvent(nameof(GameState), null);
    }
  
    private void OnGameStateChangedEvent(string type, object payload)
    {
        if (payload == null || nameof(GameState.isMapVisible).Equals(payload))
        {
            map.enabled = GameState.isMapVisible;
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnGameStateChangedEvent, nameof(GameState));
    }
}
