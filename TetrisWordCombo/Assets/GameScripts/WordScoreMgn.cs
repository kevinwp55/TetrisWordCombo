using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordScoreMgn : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject wordPanel;

    private static int PanelSize = 4;
    private static Queue<GameObject> contents;

    // Start is called before the first frame update
    void Start()
    {
        contents = new Queue<GameObject>(PanelSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayFoundWord(string word)
    {
        if(contents.Count >= PanelSize)
        {
            GameObject DequeuedPrefab = contents.Dequeue();
            Destroy(DequeuedPrefab);
        }

        GameObject newPrefab = Instantiate(textPrefab);
        newPrefab.transform.GetChild(0).GetComponent<Text>().text = word;
        newPrefab.transform.SetParent(wordPanel.transform, false);
        contents.Enqueue(newPrefab);

        StartCoroutine(AnimateNewEnqueue(newPrefab));
        StartCoroutine(StayInPanelTimer());
    }

    IEnumerator StayInPanelTimer()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        yield return wait;
        if(contents.Count > 0)
        {
            GameObject temp = contents.Dequeue();
            Destroy(temp);
        }
    }

    IEnumerator AnimateNewEnqueue(GameObject prefab)
    {
        WaitForSeconds wait = new WaitForSeconds((float)0.15);
        float timer = 0f;
        float maxTime = 1f;
        while(timer < maxTime)
        {
            if (prefab == null)
                break;
            else
            {
                prefab.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                yield return wait;
                if (prefab == null)
                    break;
                prefab.transform.GetChild(0).GetComponent<Text>().color = Color.white;
                yield return wait;
                timer += 0.15f;
            }
            
        }
        if(prefab != null)
            prefab.transform.GetChild(0).GetComponent<Text>().color = Color.white;
    }
}
