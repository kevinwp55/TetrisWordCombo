using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeBankMgn : MonoBehaviour
{
    public TextAsset[] Themes;

    private Dictionary<string, List<string>> ThemeBank;
    
    // Start is called before the first frame update
    void Start()
    {
        ThemeBank = new Dictionary<string, List<string>>();

        for (int i = 0; i < Themes.Length; i++)
        {
            //print("THEME: " + Themes[i].name);
            List<string> addTheseWords = ParseFile(i);
            ThemeBank.Add(Themes[i].name, addTheseWords);
        }

        //PrintDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<string> ParseFile(int index)
    {
        List<string> words = new List<string>();
        string text = Themes[index].text;
        string[] line = text.Split('\n');
        for(int i=0; i<line.Length; i++)
        {
            line[i] = line[i].ToUpper();
            line[i] = line[i].Trim();
        }
        words.AddRange(line);

        return words;
    }

    public List<string> GetSpecificList(string theme)
    {
        return ThemeBank[theme];
    }

    public List<string> GetThemesList()
    {
        List<string> keyList = new List<string>(ThemeBank.Keys);
        return keyList;
    }

    public string GetRandomTheme()
    {
        List<string> keyList = new List<string>(ThemeBank.Keys);
        string ReturnTheme = keyList[Random.Range(0, keyList.Count)];
        return ReturnTheme;
    }

    public string GetRandomWordFromTheme(string theme)
    {
        List<string> themeList = ThemeBank[theme];
        string word = themeList[Random.Range(0, themeList.Count)];
        //print("GET THIS WORD: " + word);
        return word;
    }

    public bool isThemedWord(string word)
    {
        return ThemeBank[FindObjectOfType<ThemeMgn>().GetTheme()].Contains(word);
    }

    #region Utilities
    void PrintDictionary()
    {
        foreach(KeyValuePair<string, List<string>> list in ThemeBank)
        {
            for (int j = 0; j < list.Value.Count; j++)
            {
                print(list.Value[j]);
            }

        }
    }

    void PrintList(List<string> list)
    {
        foreach(string child in list)
        {
            print(child);
        }
    }
    #endregion
}
