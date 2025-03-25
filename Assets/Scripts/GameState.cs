using UnityEngine;

public class GameState : MonoBehaviour
{
    #region isMapVisible
    private static bool _isMapVisible = true;
    public static bool isMapVisible
    {
        get => _isMapVisible;
        set
        {
            if (value != _isMapVisible)
            {
                _isMapVisible = value;
                GameEventSystem.EmitEvent(nameof(GameState), nameof(isMapVisible));
            }
        }
    }
    #endregion
}
