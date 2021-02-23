using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject h2pPanel;
    public GameObject SettingsPanel;
    public GameObject HighScoresPanel;
    public GameObject WordBankPanel;
    public GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
