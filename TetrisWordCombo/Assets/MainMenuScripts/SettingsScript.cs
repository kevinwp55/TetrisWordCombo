using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    public AudioMixer AudioParams;
    public Dropdown ResolutionDropDown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropDown.ClearOptions();
        List<string> resOptStrings = new List<string>();
        int currentResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resOptStrings.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDropDown.AddOptions(resOptStrings);
        ResolutionDropDown.value = currentResolutionIndex;
        ResolutionDropDown.RefreshShownValue();
    }

    public void SetMusicVolume(float volume)
    {
        AudioParams.SetFloat("MusicParam", volume);
    }

    public void SetSoundVolume(float volume)
    {
        AudioParams.SetFloat("SoundParam", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetClassicMode(bool isClassic)
    {
        PlayerPrefs.SetString("Classic", isClassic.ToString());
    }
}
