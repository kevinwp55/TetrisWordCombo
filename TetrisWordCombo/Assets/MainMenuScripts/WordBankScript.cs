using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordBankScript : MonoBehaviour
{
    public TextAsset[] themesList;
    public TextAsset[] wordsList;
    public GameObject prefab;
    public GameObject content;
    public Dropdown LetterSizeSelect;
    public Dropdown ThemeSelect;

    Dictionary<string, List<string>> ThemesOnly;
    Dictionary<int, List<string>> AllWords;
    List<GameObject> prefabs;
    // Start is called before the first frame update
    void Start()
    {
        ThemesOnly = new Dictionary<string, List<string>>();
        AllWords = new Dictionary<int, List<string>>();
        prefabs = new List<GameObject>();

        ParseWordBank();
        ParseThemedWords();

        FillWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshButton()
    {
        LetterSizeSelect.value = 0;
        ThemeSelect.value = 0;
        LetterSizeSelect.RefreshShownValue();
        ThemeSelect.RefreshShownValue();
        FillWords();
    }

    #region Fill Word Bank

    void ClearAllFromView()
    {
        foreach(GameObject child in prefabs)
        {
            Destroy(child.gameObject);
        }
    }

    public void FillWords()
    {
        ClearAllFromView();
        if(ThemeSelect.value == 0)
        {
            for (int i = 0; i < AllWords[LetterSizeSelect.value + 3].Count; i++)
            {
                GameObject newPrefab = Instantiate(prefab);
                newPrefab.transform.GetChild(0).GetComponent<Text>().text = AllWords[LetterSizeSelect.value + 3][i];
                newPrefab.transform.SetParent(content.transform);
                prefabs.Add(newPrefab);
            }
        }
        else
        {
            for (int i = 0; i < ThemesOnly[ThemeSelect.options[ThemeSelect.value].text].Count; i++)
            {
                GameObject newPrefab = Instantiate(prefab);
                newPrefab.transform.GetChild(0).GetComponent<Text>().text = ThemesOnly[ThemeSelect.options[ThemeSelect.value].text][i];
                newPrefab.transform.SetParent(content.transform);
                prefabs.Add(newPrefab);
            }
        }

    }
    #endregion

    #region Parse TextAssets
    void ParseWordBank()
    {
        for(int i=0; i<wordsList.Length; i++)
        {
            List<string> words = ParseWordFiles(i);
            AllWords.Add(i + 3, words);
        }
    }

    void ParseThemedWords()
    {
        
        for(int i=0; i<themesList.Length; i++)
        {
            List<string> words = ParseThemeFiles(i);
            ThemesOnly.Add(themesList[i].name, words);
            foreach(string word in words)
            {
                if (word.Length <= 10 && word.Length >= 3)
                    if (!AllWords[word.Length].Contains(word))
                        AllWords[word.Length].Add(word);
            }
        }
    }

    List<string> ParseThemeFiles(int index)
    {
        List<string> words = new List<string>();
        string text = themesList[index].text;
        string[] line = text.Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            line[i] = line[i].ToUpper();
            line[i] = line[i].Trim();
        }
        words.AddRange(line);

        return words;
    }

    List<string> ParseWordFiles(int index)
    {
        List<string> words = new List<string>();

        string text = wordsList[index].text;
        string[] lines = text.Split('\n');
        foreach (string line in lines)
        {
            string[] WordLine = line.Split(' ');
            for (int i = 0; i < WordLine.Length; i++)
            {
                WordLine[i] = WordLine[i].ToUpper();
                WordLine[i] = WordLine[i].Trim();
            }
            words.AddRange(WordLine);
        }

        return words;
    }
    #endregion
}
