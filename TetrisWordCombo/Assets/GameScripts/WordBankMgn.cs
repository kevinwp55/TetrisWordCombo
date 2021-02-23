using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBankMgn : MonoBehaviour
{
    public TextAsset[] WordBankFiles;

    private static Dictionary<int, List<string>> WordBank = new Dictionary<int, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        if(WordBank.Count == 0)
        {
            for (int i = 0; i < WordBankFiles.Length; i++)
            {
                List<string> addTheseWords = ParseFile(i);
                WordBank.Add(i + 3, addTheseWords);
            }

            StartCoroutine(GetThemedWords());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<string> ParseFile(int index)
    {
        List<string> words = new List<string>();

        string text = WordBankFiles[index].text; text.ToUpper();
        string[] lines = text.Split('\n');
        foreach(string line in lines)
        {
            string[] WordLine = line.Split(' ');
            for(int i=0; i<WordLine.Length; i++)
            {
                WordLine[i] = WordLine[i].ToUpper();
                WordLine[i] = WordLine[i].Trim();
            }
            words.AddRange(WordLine);
        }

        return words;
    }

    public bool FindWord(string word)
    {
        return WordBank[word.Length].Contains(word);
    }

    #region Get Themed Words
    IEnumerator GetThemedWords()
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.1);
        yield return wait;

        List<string> ThemesList = FindObjectOfType<ThemeBankMgn>().GetThemesList();
        foreach(string theme in ThemesList)
        {
            List<string> tempList = FindObjectOfType<ThemeBankMgn>().GetSpecificList(theme);
            foreach(string word in tempList)
            {
                //print("WORD: " + word + ", Word Length: " + word.Length);
                if(word.Length <= 10 && word.Length >=3)
                    if(!WordBank[word.Length].Contains(word))
                        WordBank[word.Length].Add(word);
            }
        }
    }
    #endregion

    #region Utilities
    void PrintDictionary()
    {
        for(int i=0; i<WordBank.Count; i++)
        {
            for(int j=0; j<WordBank[i].Count; j++)
            {
                print(WordBank[i][j]);
            }
        }
    }
    #endregion
}
