using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeMgn : MonoBehaviour
{
    private int CurrentLevel = 0;
    private string CurrentTheme = "";

    public Text ThemeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTheme = FindObjectOfType<ThemeBankMgn>().GetRandomTheme();
        ThemeDisplay.text = CurrentTheme;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentLevel();
        ThemeDisplay.text = CurrentTheme;
    }

    public string GetTheme()
    {
        return ThemeDisplay.text;
    }

    void CheckCurrentLevel()
    {
        int LevelCheck = FindObjectOfType<LevelMgn>().GetCurrentLevel();
        if (CurrentLevel < LevelCheck)
        {
            CurrentLevel = LevelCheck;
            while(CurrentTheme == FindObjectOfType<ThemeBankMgn>().GetRandomTheme())
            {
                CurrentTheme = FindObjectOfType<ThemeBankMgn>().GetRandomTheme();
            }
            StartCoroutine(AnimateThemeText(Color.red));
        }
    }

    IEnumerator AnimateThemeText(Color color)
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.15);
        float timer = 0.0f;
        while(timer < 1.0f)
        {
            ThemeDisplay.color = color;
            yield return wait;
            ThemeDisplay.color = Color.white;
            yield return wait;
            timer += 0.15f;
        }
        ThemeDisplay.color = Color.white;
    }
}
