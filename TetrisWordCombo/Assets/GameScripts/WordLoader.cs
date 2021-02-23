using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordLoader : MonoBehaviour
{
    private List<string> ListOfThemedWords;
    private Queue<char> WordHolder;
    private static int qSize = 20;
    // Start is called before the first frame update
    void Start()
    {
        ListOfThemedWords = new List<string>();
        WordHolder = new Queue<char>(qSize);

        QueueUpdater();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public char[] GetThemedSequence()
    {
        QueueUpdater();
        //PrintQueue();

        char[] sequence = new char[4];

        for (int i=0; i<sequence.Length; i++)
        {
            sequence[i] = WordHolder.Dequeue();
        }

        return sequence;
    }
    void QueueUpdater()
    {
        string word = FindObjectOfType<ThemeBankMgn>().GetRandomWordFromTheme(FindObjectOfType<ThemeMgn>().GetTheme());
        while (WordHolder.Count + word.Length < qSize)
        {
            AddToQueue(word);
            word = FindObjectOfType<ThemeBankMgn>().GetRandomWordFromTheme(FindObjectOfType<ThemeMgn>().GetTheme());
        }
    }

    void AddToQueue(string word)
    {
        foreach (char letter in word)
        {
            WordHolder.Enqueue(letter);
        }
    }

    #region Utilities
    void PrintQueue()
    {
        string WordHolderString = "";
        foreach(char letter in WordHolder)
        {
            WordHolderString += letter.ToString();
        }
        print(WordHolderString);
    }
    #endregion
}
