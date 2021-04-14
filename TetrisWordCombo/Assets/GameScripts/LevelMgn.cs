using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LevelMgn : MonoBehaviour
{
    private int CurrentLevel;
    private int[] ScoreBounds;
    private bool ifLevelUp;

    public Text PlayerCurrentLevel;
    public Text LevelUpText;
    public AudioSource levelUpSound;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = 0;
        ifLevelUp = false;
        LevelUpText.text = "";

        ScoreBounds = new int[]
        {
            2500, 5000, 7500, 10000,
            15000, 20000, 25000, 30000,
            40000, 50000, 60000, 70000,
            95000, 120000, 145000, 170000,
        };
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCurrentLevel.text = CurrentLevel.ToString();

        CompareScore(FindObjectOfType<ScoreMgn>().GetScore());

        LevelUp();

    }

    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }

    void CompareScore(int score)
    {
        for(int i=0; i<ScoreBounds.Length; i++)
        {
            if(score > ScoreBounds[i] && CurrentLevel < i+1)
            {
                ifLevelUp = true;
            }
        }
    }

    void LevelUp()
    {
        if(ifLevelUp)
        {
            CurrentLevel += 1;
            ifLevelUp = false;
            levelUpSound.Play();
            StartCoroutine(AnimateLevelUpText());
        }
    }

    IEnumerator AnimateLevelUpText()
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.15);
        float timer = 0.0f;
        while(timer < 1.0f)
        {
            LevelUpText.text = "Level Up!";
            yield return wait;
            LevelUpText.text = "";
            yield return wait;
            timer += 0.15f;
        }
        LevelUpText.text = "";
    }

}

