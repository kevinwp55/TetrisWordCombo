using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundScript : MonoBehaviour
{
    public VideoClip[] videos;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VideoPlayer>().clip = videos[Random.Range(0, videos.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
