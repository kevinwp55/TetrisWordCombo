using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    public AudioMixer AudioParams;
    public Dropdown ResolutionDropDown;
    public Slider MusicSlider;
    public Slider SoundSlider;
    public Text MusicText;
    public Text SoundText;
    public Toggle FullScreenToggle;
    public Toggle ClassicToggle;

    Resolution[] resolutions;
    bool ClassicMode = false;
    bool FullScreenMode = true;

    void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropDown.ClearOptions();
        List<string> resOptStrings = new List<string>();
        int currentResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if(!resOptStrings.Contains(option))
                resOptStrings.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropDown.AddOptions(resOptStrings);
        ResolutionDropDown.value = currentResolutionIndex;
        ResolutionDropDown.RefreshShownValue();

        GetPresetAudioParams();
        GetPresetToggleParams();
    }

    void GetPresetAudioParams()
    {
        float music = PlayerPrefs.GetFloat("MusicVol", (float)0.0);
        float sound = PlayerPrefs.GetFloat("SoundVol", (float)0.0);
        MusicText.text = ((int)music + 80).ToString() + "%";
        SoundText.text = ((int)sound + 80).ToString() + "%";
        MusicSlider.value = music;
        SoundSlider.value = sound;
    }

    void GetPresetToggleParams()
    {
        ClassicMode = bool.Parse(PlayerPrefs.GetString("Classic", "false"));
        FullScreenMode = bool.Parse(PlayerPrefs.GetString("FullScreen", "true"));
        FullScreenToggle.isOn = FullScreenMode;
        ClassicToggle.isOn = ClassicMode;
    }

    public void SetMusicVolume(float volume)
    {
        AudioParams.SetFloat("MusicParam", volume);
        MusicText.text = ((int)volume + 80).ToString() + "%";
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void SetSoundVolume(float volume)
    {
        AudioParams.SetFloat("SoundParam", volume);
        SoundText.text = ((int)volume + 80).ToString() + "%";
        PlayerPrefs.SetFloat("SoundVol", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        FullScreenMode = isFullScreen;
        print(isFullScreen.ToString());
        PlayerPrefs.SetString("FullScreen", FullScreenMode.ToString());
    }

    public void SetClassicMode(bool isClassic)
    {
        ClassicMode = !ClassicMode;
        print(ClassicMode.ToString());
        PlayerPrefs.SetString("Classic", ClassicMode.ToString());
    }

    
    public void SetResolution(int i)
    {
        Resolution res = resolutions[i];
        Screen.SetResolution(res.width, res.height, FullScreenMode, 60);
        PlayerPrefs.SetInt("ResolutionWidth", res.width);
        PlayerPrefs.SetInt("ResolutionHeight", res.height);
    }

    public void UpdatePlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}
