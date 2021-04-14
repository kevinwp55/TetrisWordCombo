using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public GameObject[] cPanels;
    public Text ShowPanelText;
    Button NextButton;
    Button PrevButton;
    int PanelIndex;

    // Start is called before the first frame update
    void Start()
    {
        PanelIndex = 0;
        setPanelActive(PanelIndex);
        ShowPanelText.text = "1 / 4";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setPanelActive(int index)
    {
        for(int i=0; i<cPanels.Length; i++)
        {
            cPanels[i].SetActive(false);
        }

        if(index >= cPanels.Length)
        {
            cPanels[cPanels.Length - 1].SetActive(true);
        }
        cPanels[index].SetActive(true);
    }

    public void Next()
    {
        if (PanelIndex < cPanels.Length)
        {
            PanelIndex++;
            setPanelActive(PanelIndex);
            ShowPanelText.text = (PanelIndex + 1).ToString() + " / 4";
        }
    }

    public void Prev()
    {
        if(PanelIndex > 0)
        {
            PanelIndex--;
            setPanelActive(PanelIndex);
            ShowPanelText.text = (PanelIndex + 1).ToString() + " / 4";
        }
    }
}
