using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseScript : MonoBehaviour
{
    public GameObject PausePanel;
    public AudioMixer AudioParams;
    private float UnpausedVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PausePanel.gameObject.activeSelf)
        {
            Time.timeScale = 0;
            AudioParams.GetFloat("MusicParam", out UnpausedVolume);
            if(!(UnpausedVolume <= -30.0))
                AudioParams.SetFloat("MusicParam", (float) -30.0);
            PausePanel.gameObject.SetActive(true);
        }

        else if((Input.GetKeyDown(KeyCode.Escape) && PausePanel.gameObject.activeSelf))
        {
            Time.timeScale = 1;
            AudioParams.SetFloat("MusicParam", UnpausedVolume);
            PausePanel.gameObject.SetActive(false);
        }
    }

    #region Pause Buttons

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PausePanel.gameObject.SetActive(false);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    #endregion
}
