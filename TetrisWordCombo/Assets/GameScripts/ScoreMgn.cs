using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMgn : MonoBehaviour
{
    public Text PlayerCurrentScore;
    public Text PlayerAddedScore;

    private int currentScore;
    private int addedScore;
    private List<string> PlayerScoredWords;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        addedScore = 0;
        PlayerScoredWords = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCurrentScore.text = currentScore.ToString();
        if (addedScore > 0)
        {
            PlayerAddedScore.text = addedScore.ToString();
        }

        if(addedScore <= 0)
        {
            PlayerAddedScore.text = "";
        }
    }

    public void AwardPoints(string word)
    {
        int points = 0;
        int multiplier = 1;
        if (word == "")
            points = 100;
        else if (word == null)
            points = 100;
        else
        {
            points = 25;
            multiplier = word.Length;
            if (FindObjectOfType<ThemeBankMgn>().isThemedWord(word))
                multiplier *= 2;
            PlayerScoredWords.Add(word);
            //print("WORD: " + word + ", AWARD: " +points * multiplier);
        }

        addedScore += points * multiplier;
        StartCoroutine(TransferScore());
    }

    IEnumerator TransferScore()
    {
        WaitForSeconds wait = new WaitForSeconds((float) 0.015);
        while(addedScore > 0)
        {
            currentScore++;
            addedScore--;
            yield return wait;
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetAddedScore()
    {
        return addedScore;
    }

    public List<string> GetWordsList()
    {
        return PlayerScoredWords;
    }
}
