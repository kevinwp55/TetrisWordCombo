using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject h2pPanel;
    public GameObject SettingsPanel;
    public GameObject HighScoresPanel;
    public GameObject WordBankPanel;
    public GameObject CreditsPanel;
    public AudioMixer AudioParams;

    // Start is called before the first frame update
    void Start()
    {
        float music = PlayerPrefs.GetFloat("MusicVol", (float)0.0);
        float sound = PlayerPrefs.GetFloat("SoundVol", (float)0.0);
        AudioParams.SetFloat("MusicParam", music);
        AudioParams.SetFloat("SoundParam", sound);

        PlayerPrefs.GetString("Classic", "false");
        //Screen.fullScreen = bool.Parse(PlayerPrefs.GetString("FullScreen", "true"));

        //Screen.SetResolution(PlayerPrefs.GetInt("ResolutionWidth", 2560), PlayerPrefs.GetInt("ResolutionHeight", 1440), bool.Parse(PlayerPrefs.GetString("FullScreen", "true")), 60);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region MainMenu Buttons

    public void ShowHowToPlay()
    {
        h2pPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        SettingsPanel.SetActive(true);
    }

    public void ShowHighScores()
    {
        HighScoresPanel.SetActive(true);
    }

    public void ShowWordBank()
    {
        WordBankPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    #endregion

    #region BackButtons

    public void BackH2P()
    {
        h2pPanel.SetActive(false);
    }

    public void BackSettings()
    {
        SettingsPanel.SetActive(false);
    }

    public void BackHighScores()
    {
        HighScoresPanel.SetActive(false);
    }

    public void BackWordBank()
    {
        WordBankPanel.SetActive(false);
    }

    public void BackCredits()
    {
        CreditsPanel.SetActive(false);
    }

    #endregion
}
