using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScoreScript : MonoBehaviour
{
    public Text[] input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckNameInput()
    {
        foreach(Text child in input)
        {
            if (child.text == null)
                return false;
            else if (child.text == "")
                return false;
        }
        return true;
    }

    public string GetPlayerName()
    {
        string conc_name = "";
        foreach(Text child in input)
        {
            conc_name += child.text;
        }
        conc_name = conc_name.ToUpper();
        return conc_name;
    }
}
