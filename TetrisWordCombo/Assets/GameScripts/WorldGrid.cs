using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Block
{
    private string letter;
    private bool HighlightFlag;

    public Block(string l) { letter = l; HighlightFlag = false; }
    public string GetLetter() { return letter;  }
    public bool isBlockFlagged() { return HighlightFlag; }
    public void FlagBlock() { HighlightFlag = true; }
}

struct BlockCoord
{
    private int xCoord;
    private int yCoord;

    public BlockCoord(int x, int y) { xCoord = x; yCoord = y; }
    public int GetX() { return xCoord; }
    public int GetY() { return yCoord; }
}

public class WorldGrid : MonoBehaviour
{
    private static Transform[,] Grid;
    private static Block[,] LetterGrid;

    private static int height = 20;
    private static int width = 10;
    private static Queue<string> wordList;

    // Start is called before the first frame update
    void Start()
    {
        Grid = new Transform[width, height];
        LetterGrid = new Block[width, height];
        wordList = new Queue<string>(30);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWordAward();
    }

    #region Get Dimensions

    public int GetHeight()
    {
        return height;
    }

    public int GetWidth()
    {
        return width;
    }

    #endregion

    #region Classic Tetris Methods

    public bool AddToGrid(GameObject tetromino)
    {
        foreach (Transform child in tetromino.transform)
        {
            int roundedX = Mathf.RoundToInt((child.transform.position.x));
            int roundedY = Mathf.RoundToInt((child.transform.position.y));
            try
            {
                Grid[roundedX, roundedY] = child;
                LetterGrid[roundedX, roundedY] = new Block(child.GetComponent<SpriteRenderer>().gameObject.transform.GetChild(0).GetComponent<TextMesh>().text);
            }
            catch(System.IndexOutOfRangeException)
            {
                return false;
            }            
        }
        //PrintLetterGrid();
        return true;
    }

    public void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                FlagLine(i);
                FindObjectOfType<ScoreMgn>().AwardPoints(null);
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (Grid[j, i] == null)
                return false;
        }
        return true;
    }

    void FlagLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            FlagBlock(j, i);
        }
    }

    void FlagBlock(int x, int y)
    {
        LetterGrid[x, y].FlagBlock();
    }

    public Transform[,] UpdateWorldGrid()
    {
        return Grid;
    }

    public bool IsGameOver()
    {
        for (int x = 0; x < width; x++)
        {
            if (Grid[x, height - 1] != null)
                return true;
        }
        return false;
    }

    public void InitAnimateAndDelete()
    {
        HighlightFlaggedBlocks();
        StartCoroutine(DeleteBlocks());
    }

    #endregion

    #region Word Combo Methods

    public void CheckForWords()
    {
        for(int j=0; j<height; j++)
        {
            for(int i=0; i<width; i++)
            {
                if(LetterGrid[i,j] != null)
                    FindWordAt(i,j);
            }
        }
    }

    void FindWordAt(int x, int y)
    {
        if (Grid[x, y] != null)
        {
            //Look right
            //Start at letter size 3 and stop at letter size 10
            for (int counter = 3; counter + x < width && counter < 10; counter++)
            {
                string word = "";
                List<BlockCoord> wordBlock = new List<BlockCoord>();
                for (int i = 0; i < counter; i++)
                {
                    if (Grid[i+x, y] == null)
                        break;
                    else
                    {
                        word += LetterGrid[i+x, y].GetLetter();
                        wordBlock.Add(new BlockCoord(i + x, y));
                    }
                }
                if(word.Length >= 3)
                {
                    if(FindObjectOfType<WordBankMgn>().FindWord(word))
                    {
                        foreach(BlockCoord child in wordBlock)
                        {
                            FlagBlock(child.GetX(), child.GetY());
                        }
                        AddWordToAwardQueue(word);
                    }
                }
                word = "";
                wordBlock.Clear();
            }

            //Look Down
            //Start at letter size 3 and stop at letter size 10
            for(int counter = 3; counter + y < height && counter < 10; counter++)
            {
                string word = "";
                List<BlockCoord> wordBlock = new List<BlockCoord>();
                for (int j = 0; j < counter; j++)
                {
                    if (Grid[x, j + y] == null)
                        break;
                    else
                    {
                        word += LetterGrid[x,j + y].GetLetter();
                        wordBlock.Add(new BlockCoord(x, j + y));
                    }
                }
                if (word.Length >= 3)
                {
                    word = ReverseString(word);
                    if (FindObjectOfType<WordBankMgn>().FindWord(word))
                    {
                        foreach (BlockCoord child in wordBlock)
                        {
                            FlagBlock(child.GetX(), child.GetY());
                        }
                        AddWordToAwardQueue(word);
                    }
                }
                word = "";
                wordBlock.Clear();
            }
        }
    }

    string ReverseString(string s)
    {
        char[] charArray = s.ToCharArray();
        string newString = "";
        for(int i=charArray.Length-1; i >= 0; i--)
        {
            newString += charArray[i];
        }

        return newString;
    }

    void AddWordToAwardQueue(string word)
    {
        if(!wordList.Contains(word))
            wordList.Enqueue(word);
    }

    void UpdateWordAward()
    {
        if(wordList.Count > 0)
        {
            string word = wordList.Dequeue();
            FindObjectOfType<ScoreMgn>().AwardPoints(word);
            FindObjectOfType<WordScoreMgn>().DisplayFoundWord(word);
        }
    }

    #endregion

    #region Animate Highlight

    void HighlightFlaggedBlocks()
    {
        Color[,] BlockColors = new Color[width, height];
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (LetterGrid[i, j] != null)
                {
                    if (LetterGrid[i, j].isBlockFlagged())
                    {
                        StartCoroutine(AnimateBlockColors(i, j));
                    }
                }
            }
        }
    }

    IEnumerator AnimateBlockColors(int x, int y)
    {
        Color blockColor = Grid[x, y].GetComponent<SpriteRenderer>().color;
        WaitForSeconds ColorSwapTime = new WaitForSeconds((float)0.075);
        float maxTime = 1.0f;
        float timer = 0.0f;
        while (timer < maxTime)
        {
            Grid[x, y].GetComponent<SpriteRenderer>().color = Color.white;
            Grid[x, y].GetComponent<SpriteRenderer>().gameObject.transform.GetChild(0).GetComponent<TextMesh>().color = Color.black;
            yield return ColorSwapTime;

            Grid[x, y].GetComponent<SpriteRenderer>().color = blockColor;
            Grid[x, y].GetComponent<SpriteRenderer>().gameObject.transform.GetChild(0).GetComponent<TextMesh>().color = Color.white;
            yield return ColorSwapTime;
            timer += 0.4f;
        }
    }

    #endregion

    #region Delete Blocks and Row Drop

    IEnumerator DeleteBlocks()
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.5);
        yield return wait;
        for (int j = height-1; j >= 0; j--)
        {
            for(int i=0; i<width; i++)
            {
                if(LetterGrid[i,j] != null)
                {
                    if(LetterGrid[i,j].isBlockFlagged())
                    {
                        DeleteBlock(i, j);
                        DropBlock(i, j);
                    }
                }
            }
        }
    }

    void DeleteBlock(int x, int y)
    {
        Destroy(Grid[x, y].gameObject);
        Grid[x,y] = null;
        LetterGrid[x,y] = null;
    }

    void DropBlock(int x, int y)
    {
        for (int j = y; j < height; j++)
        {
            if (LetterGrid[x, j] != null)
            {
                Grid[x, j - 1] = Grid[x, j];
                LetterGrid[x, j - 1] = LetterGrid[x, j];
                Grid[x, j] = null;
                LetterGrid[x, j] = null;
                Grid[x, j - 1].transform.position -= new Vector3(0, 1, 0);
            }
        }
    }

    #endregion


    #region Utilities

    void PrintLetterGrid()
    {
        string row = "";
        for(int j=height-1; j>=0; j--)
        {
            for(int i=0; i<width; i++)
            {
                if (LetterGrid[i, j] == null)
                {
                    row += "#";
                }
                else
                {
                    if (LetterGrid[i, j].isBlockFlagged())
                        row += "@";
                    else
                        row += LetterGrid[i, j].GetLetter();
                }
            }
            print(row);
            row = "";
        }
    }

    #endregion
}

/*for(int j = y; j<height; j++)
        {
            if(LetterGrid[x, j] != null)
            {
                Grid[x, j-1] = Grid[x,j];
                LetterGrid[x, j - 1] = LetterGrid[x,j];
                Grid[x,j] = null;
                LetterGrid[x,j] = null;
                Grid[x, j-1].transform.position -= new Vector3(0, 1, 0);
            }
        }
 * 
 * 
 */
