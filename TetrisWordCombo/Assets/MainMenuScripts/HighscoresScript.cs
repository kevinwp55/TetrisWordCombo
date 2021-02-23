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

    List<GameObject> prefabs;
    DBManager dbHandler;
    private static int max = 50;

    // Start is called before the first frame update
    void Start()
    {
        prefabs = new List<GameObject>();
        dbHandler = FindObjectOfType<DBManager>();

        FillHighScores();
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

    void FillHighScores()
    {
        List<Stats> highscores = new List<Stats>();
        highscores = dbHandler.GetHighscores();
        highscores = highscores.OrderBy(x => int.Parse(x.score)).ToList();
        highscores.Reverse();
        
        for(int i=0; i<max; i++)
        {
            if (i >= highscores.Count)
                break;
            if (highscores.Count == 0)
                break;
            GameObject newPrefab = Instantiate(HighscoresPrefab);
            newPrefab.transform.Find("RankText").GetComponent<Text>().text = (i+1).ToString();
            newPrefab.transform.Find("NameText").GetComponent<Text>().text = highscores[i].username;
            newPrefab.transform.Find("ScoreText").GetComponent<Text>().text = highscores[i].score;
            newPrefab.transform.Find("LevelText").GetComponent<Text>().text = highscores[i].level;
            newPrefab.transform.Find("WordsText").GetComponent<Text>().text = highscores[i].words;
            /*
            foreach(string word in highscores[i].wordlist)
            {
                GameObject WordPrefab = Instantiate(WordsListPrefab);
                WordPrefab.transform.GetChild(0).GetComponent<Text>().text = word;
                WordPrefab.transform.SetParent(newPrefab.transform.Find("WordList").transform.Find("Content").GetComponent<RectTransform>().transform);
            }*/
            newPrefab.transform.Find("TimeText").GetComponent<Text>().text = highscores[i].playtime;
            newPrefab.transform.Find("DateText").GetComponent<Text>().text = highscores[i].date;
            newPrefab.transform.SetParent(contentBlock.transform);
            prefabs.Add(newPrefab);
        }
    }
}
