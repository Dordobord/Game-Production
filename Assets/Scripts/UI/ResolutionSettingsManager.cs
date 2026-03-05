using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ResolutionSettingsManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    Resolution[] allResolutions;
    bool isFullscreen;
    int selectedResolution;
    List<Resolution> SelectedResolutionList = new List<Resolution>();
    void Start()
    {
        isFullscreen = true;
        allResolutions = Screen.resolutions;

        List<string> options = new List<string>();
        string newResolution;
        
        foreach (Resolution res in allResolutions)
        {
            newResolution = res.width.ToString() + " x " + res.height.ToString();
            if (!options.Contains(newResolution))
            {
                options.Add(newResolution);
                SelectedResolutionList.Add(res);
            }
            options.Add(res.ToString());
        }

        resolutionDropdown.AddOptions(options);
    }

    public void ChangeResolution()
    {
        selectedResolution += resolutionDropdown.value;
        Screen.SetResolution(SelectedResolutionList[selectedResolution].width, SelectedResolutionList[selectedResolution].height, isFullscreen);
    }

    public void ToggleFullscreen()
    {
        isFullscreen = fullscreenToggle.isOn;
        Screen.SetResolution(SelectedResolutionList[selectedResolution].width, SelectedResolutionList[selectedResolution].height, isFullscreen);
    }

}
