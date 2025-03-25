using System;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour
{
    private Toggle Map;
    [SerializeField]
    private TMPro.TMP_Dropdown graphicsDropdown;
    [SerializeField]
    private TMPro.TMP_Dropdown fogDropdown;
    void Start()
    {
        Transform togglesLayout = transform.Find("menu/OptionsMenu/DifficultySection/ToggleLayout");
        Map = togglesLayout.Find("Map/MapToggle").GetComponent<Toggle>();
        Map.isOn = GameState.isMapVisible;

        Transform layout = this.transform.Find("menu/OptionsMenu/DifficultySection/Layout");

        #region Graphics
        //graphicsDropdown = layout
        //    .Find("Graphics/")
        //    .GetComponent<TMPro.TMP_Dropdown>();
        graphicsDropdown.ClearOptions();
        foreach (string name in QualitySettings.names)
        {
            graphicsDropdown.options.Add(new(name));
        }

        int currentLevel = QualitySettings.GetQualityLevel();
        graphicsDropdown.value = currentLevel;
        #endregion

        #region Fog
        //fogDropdown = layout
        //     .Find("Fog/")
        //    .GetComponent<TMPro.TMP_Dropdown>();
        fogDropdown.ClearOptions();
        fogDropdown.options.Add(new("Off"));
        foreach (var value in Enum.GetValues(typeof(FogMode)))
        {
            fogDropdown.options.Add(new(value.ToString()));
        }
        fogDropdown.value = (int)RenderSettings.fogMode;
        #endregion
    }

    public void OnMapToggleChanged(bool value)
    {
        GameState.isMapVisible = value;
    }
    public void OnQualityDropdownChanged(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }

    public void OnFogDropdownChanged(int index)
    {
        if (index == 0)
        {
            RenderSettings.fog = false;
        }
        else
        {
            RenderSettings.fog = true;
            RenderSettings.fogMode = (FogMode)index;
        }
    }

}