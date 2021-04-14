using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameOverScript : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject MainPanel;
    public GameObject ContinuePanel;
    public GameObject DisplayWordsListPanel;
    public GameObject WordsPanelContents;
    public GameObject ContentPrefab;
    public Button SaveScoreButton;
    public AudioMixer AudioParams;
    public AudioSource GameOverSound;

    public Text SaveScoreSuccessText;
    public Text PlayerScore;
    public Text PlayerLevel;
    public Text PlayerWords;
    public Text PlayerTime;

    private bool GameOver = false;
    private List<string> wordsList;
    private string NameInputError = "Invalid name, empty input or inappropriate name...";
    private string ConnectionError = "Score could not be saved due to connection error...";
    private string SaveScoreSuccess = "Score successfully saved!";
    private float UnpausedVolume;

    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;
        wordsList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableGameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        GameOver = true;
        AudioParams.GetFloat("MusicParam", out UnpausedVolume);
        GameOverSound.Play();
        if (!(UnpausedVolume <= -30.0))
            AudioParams.SetFloat("MusicParam", (float)-30.0);
        DisplayPlayerStats();
    }

    public void DisableGameOver()
    {
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        AudioParams.SetFloat("MusicParam", UnpausedVolume);
        GameOver = false;
    }

    public bool CheckIfGameOver()
    {
        return GameOver;
    }

    #region Game Over Buttons

    /*      Main Panel Buttons
     * 
     */
    public void ContinueButton()
    {
        MainPanel.SetActive(false);
        ContinuePanel.SetActive(true);
    }

    public void SaveScoresButton()
    {
        if (FindObjectOfType<SaveScoreScript>().CheckNameInput())
        {
            if (FindObjectOfType<DBManager>().InsertPlayerScore(
                FindObjectOfType < SaveScoreScript>().GetPlayerName(),
                PlayerScore.text,
                PlayerLevel.text,
                PlayerWords.text,
                wordsList,
                PlayerTime.text,
                GetDate()
            ))
            {
                SaveScoreSuccessText.text = SaveScoreSuccess;
                SaveScoreButton.interactable = false;
            }
            else
            {
                SaveScoreSuccessText.text = ConnectionError;
            }
        }
        else
        {
            SaveScoreSuccessText.text = NameInputError;
        }

    }

    /*      Continue Panel Buttons
     * 
     */
    public void BackButton()
    {
        MainPanel.SetActive(true);
        ContinuePanel.SetActive(false);
        DisplayWordsListPanel.SetActive(false);
    }

    /*      Display Words Panel
     * 
     */
    public void DisplayWordsButton()
    {
        MainPanel.SetActive(false);
        DisplayWordsListPanel.SetActive(true);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    #endregion

    #region GameOverPanel Methods

    void DisplayPlayerStats()
    {
        wordsList = FindObjectOfType<ScoreMgn>().GetWordsList();

        PlayerScore.text = (FindObjectOfType<ScoreMgn>().GetScore() + FindObjectOfType<ScoreMgn>().GetAddedScore()).ToString();
        PlayerLevel.text = FindObjectOfType<LevelMgn>().GetCurrentLevel().ToString();
        PlayerWords.text = (wordsList.Count).ToString();
        PlayerTime.text = FindObjectOfType<TimerMgn>().GetCurrentTime();

        FillWordsList();
    }

    void FillWordsList()
    {
        for (int i = 0; i < wordsList.Count; i++)
        {
            GameObject newPrefab = Instantiate(ContentPrefab);
            newPrefab.transform.GetChild(0).GetComponent<Text>().text = wordsList[i];
            newPrefab.transform.SetParent(WordsPanelContents.transform, false);
        }
    }

    #endregion

    #region Utilities

    string GetDate()
    {
        string current = System.DateTime.Now.ToString("MM/dd/yyyy");
        return current;
    }

    #endregion
}