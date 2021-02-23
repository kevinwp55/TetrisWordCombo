using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject PausePanel;

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
            PausePanel.gameObject.SetActive(true);
        }

        else if((Input.GetKeyDown(KeyCode.Escape) && PausePanel.gameObject.activeSelf))
        {
            Time.timeScale = 1;
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
