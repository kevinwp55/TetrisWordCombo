using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadPlayerPrefsScript : MonoBehaviour
{
    public AudioMixer AudioParams;
    // Start is called before the first frame update
    void Start()
    {
        float music = PlayerPrefs.GetFloat("MusicVol", (float)0.0);
        float sound = PlayerPrefs.GetFloat("SoundVol", (float)0.0);

        AudioParams.SetFloat("MusicParam", music);
        AudioParams.SetFloat("SoundParam", sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
