using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBlockMgn : MonoBehaviour
{
    public Sprite[] BlockImages;
    public GameObject NextBlockImage;

    // Start is called before the first frame update
    void Start()
    {
        DisplayNextBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayNextBlock()
    {
        NextBlockImage.GetComponent<Image>().sprite = BlockImages[Random.Range(0, BlockImages.Length)];
    }

    public string GetNextBlockName()
    {
        string name = NextBlockImage.GetComponent<Image>().sprite.name;
        DisplayNextBlock();
        return name;
    }
}
