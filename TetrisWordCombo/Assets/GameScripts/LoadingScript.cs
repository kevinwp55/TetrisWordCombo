using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    public Text PercentageText;
    public Text TextAnimationText;

    public void LoadScene(int SceneIndex)
    {
        LoadingPanel.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncronously(SceneIndex));
        StartCoroutine(AsyncAnimateText());
        Time.timeScale = 1;
    }

    IEnumerator LoadAsyncronously(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingSlider.value = progress;
            PercentageText.text = progress * 100f + "%";
            yield return null;
        }
    }

    IEnumerator AsyncAnimateText()
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.33);
        int count = 0;
        while(LoadingPanel.gameObject.activeSelf)
        {
            TextAnimationText.text += ".";
            count++;
            if(count >= 5)
            {
                TextAnimationText.text = "";
            }
            yield return wait;
        }
    }
}
