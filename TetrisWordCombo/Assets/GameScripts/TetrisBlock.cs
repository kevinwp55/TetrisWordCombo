using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float PrevTime;
    private float FallTime = 0.8f;

    public static int height;
    public static int width;
    public Vector3 rotationPoint;

    private static Transform[,] BlockGrid = new Transform[width, height];
    private GameObject gmWorldGrid;
    private WorldGrid wgScript;

    // Start is called before the first frame update
    void Start()
    {
        gmWorldGrid = GameObject.Find("GameManager");
        wgScript = gmWorldGrid.GetComponent<WorldGrid>();

        BlockGrid = wgScript.UpdateWorldGrid();
        height = wgScript.GetHeight();
        width = wgScript.GetWidth();

        StartCoroutine(LoadLetters());
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindObjectOfType<GameOverScript>().CheckIfGameOver())
        {
            //Move Left; if not valid move, undo move
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(-1, 0, 0);
            }

            //Move Right; if not valid move, undo move
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                    transform.position -= new Vector3(1, 0, 0);
            }

            //Rotate Tetromino, Grandchildren of tetromino (text) will always remain facing upwards [Foreach]
            //If rotation block goes out of bounds, revert rotation.
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                foreach(Transform child in transform)
                {
                    child.gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
                }
                
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                    foreach (Transform child in transform)
                    {
                        child.gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, gameObject.transform.rotation.z * -1f);
                    }
                }
            }

            //Instnant Drop Tetromino
            //Searches for lowest y-position within tetromino bounds that can support tetromino shape
            //Will instantly teleport tetromino to said position in accordance to tetromino's rotation
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                InstantDropTetromino();
            }

            /*      Move Down in relation to fall time and natural fall time.
             *      If not valid move, assume tetromino has reached the farthest bottom row with collision to other blocks.
             *      Call check for lines in World Grid script, sweeps Grid World for full width (10) not null.
             *      Disable prefab instanced script. Check for highest row of Grid World if not null, returns Game Over Screen.
             *      Try Catch new tetromino, if breaks collision or Index Out of Range, returns Game Over Screen.
             */
            if (Time.time - PrevTime > (Input.GetKey(KeyCode.S) ? FallTime / 10 : FallTime) || Time.time - PrevTime > (Input.GetKey(KeyCode.DownArrow) ? FallTime / 10 : FallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    if(wgScript.AddToGrid(this.gameObject))
                    {
                        wgScript.CheckForLines();
                        wgScript.CheckForWords();
                        wgScript.InitAnimateAndDelete();
                        this.enabled = false;
                        //Check if any blocks have reached max height of Grid World; if true, init Game Over Screen
                        if (wgScript.IsGameOver())
                            FindObjectOfType<GameOverScript>().EnableGameOver();
                        //Try to create new Tetromino in world; if exception, init GameOver Screen
                        try
                        {
                            FindObjectOfType<SpawnScript>().NewTetromino();
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            FindObjectOfType<GameOverScript>().EnableGameOver();
                        }
                    }
                    else
                        FindObjectOfType<GameOverScript>().EnableGameOver();
                }

                //Fall Time
                PrevTime = Time.time;
            }
        }
    }

    //Collision Checker
    public bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt((child.transform.position.x));
            int roundedY = Mathf.RoundToInt((child.transform.position.y));

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                return false;

            if (BlockGrid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    //Load Letters foreach newly spawned tetromino block
    IEnumerator LoadLetters()
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.0005);
        yield return wait;

        char[] loader = FindObjectOfType<WordLoader>().GetThemedSequence();

        int counter = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = loader[counter].ToString();
            counter++;
        }
    }

    //Searches for Y-Position within tetromino X-bounds
    void InstantDropTetromino()
    {
        while (ValidMove())
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                break;
            }
        }
    }
}
