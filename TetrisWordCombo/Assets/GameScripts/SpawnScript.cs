using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] Tetrominos;
    // Start is called before the first frame update
    void Start()
    {
        NewTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewTetromino()
    {
        Instantiate(FindTetrominoWithName(FindObjectOfType<NextBlockMgn>().GetNextBlockName()), transform.position, Quaternion.identity);
    }

    GameObject FindTetrominoWithName(string name)
    {
        for(int i=0; i<Tetrominos.Length; i++)
        {
            if(name == Tetrominos[i].name)
            {
                return Tetrominos[i];
            }
        }
        return Tetrominos[Random.Range(0, Tetrominos.Length)];
    }
}
