using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighscoresScript : MonoBehaviour
{
    public GameObject HighscoresPrefab;
    public GameObject WordsListPrefab;
    public GameObject contentBlock;
    public InputField searchText;
    public Button SearchB;
    public Button RefreshB;
    public Text SearchErrorText;

    List<GameObject> prefabs;
    List<Stats> highscores;
    DBManager dbHandler;
    private static int max = 50;

    // Start is called before the first frame update
    void Start()
    {
        prefabs = new List<GameObject>();
        dbHandler = FindObjectOfType<DBManager>();
        highscores = new List<Stats>();
        highscores = dbHandler.GetHighscores();
        highscores = highscores.OrderBy(x => int.Parse(x.score)).ToList();
        highscores.Reverse();

        FillHighScores(null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearAllFromView()
    {
        foreach (GameObject child in prefabs)
        {
            Destroy(child.gameObject);
        }
    }

    void FillHighScores(string name)
    {
        ClearAllFromView();
        List<Stats> filteredList = new List<Stats>();
        if (name != null)
        {
            foreach (Stats child in highscores)
            {
                if (child.username == name)
                    filteredList.Add(child);
            }
        }
        else
            filteredList = highscores;

        for(int i=0; i<max; i++)
        {
            if (i >= filteredList.Count)
                break;
            if (filteredList.Count == 0)
                break;
            GameObject newPrefab = Instantiate(HighscoresPrefab);
            if(newPrefab.transform.Find("RankText").GetComponent<Text>().text == "0")
            {
                newPrefab.transform.Find("RankText").GetComponent<Text>().text = (i + 1).ToString();
            }
            newPrefab.transform.Find("NameText").GetComponent<Text>().text = filteredList[i].username;
            newPrefab.transform.Find("ScoreText").GetComponent<Text>().text = filteredList[i].score;
            newPrefab.transform.Find("LevelText").GetComponent<Text>().text = filteredList[i].level;
            newPrefab.transform.Find("WordsText").GetComponent<Text>().text = filteredList[i].words;
            Dropdown prefabDropdown = newPrefab.transform.Find("WordListDropdown").GetComponent<Dropdown>();
            FillDropdown(prefabDropdown, filteredList[i].wordlist);
            newPrefab.transform.Find("TimeText").GetComponent<Text>().text = filteredList[i].playtime;
            newPrefab.transform.Find("DateText").GetComponent<Text>().text = filteredList[i].date;
            newPrefab.transform.SetParent(contentBlock.transform, false);
            prefabs.Add(newPrefab);
        }
    }

    void FillDropdown(Dropdown drop, List<string> words)
    {
        drop.ClearOptions();
        drop.AddOptions(words);
        drop.RefreshShownValue();
    }

    #region Search User

    public void SearchButton()
    {
        string search = searchText.text;
        search = search.ToUpper();
        if (CheckIfValidEntry())
            FillHighScores(search);
        else
        {
            SearchErrorText.text = "Invalid username";
            StartCoroutine(ClearSearchErrorText());
        }
    }

    bool CheckIfValidEntry()
    {
        if (!(searchText.text.Length == 5))
            return false;
        return true;
    }

    IEnumerator ClearSearchErrorText()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        yield return wait;
        SearchErrorText.text = "";
    }

    public void RefreshButton()
    {
        FillHighScores(null);
    }

    #endregion
}
