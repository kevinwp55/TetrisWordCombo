using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMgn : MonoBehaviour
{
    public Text timeDisplayed;

    private float PlayTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayTime = Time.time;

        int second = (int) Mathf.Floor(PlayTime);
        int minute = second / 60;
        int hour = minute / 60;

        timeDisplayed.text = LeadingZero(hour) + ":" + LeadingZero(minute) + ":" + LeadingZero(second % 60);
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public string GetCurrentTime()
    {
        return timeDisplayed.text;
    }
}