using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicVolume : MonoBehaviour
{
    public AudioMixer Mixer;
    public string musicORsfx;
    public string key;

    private void Start()
    {
        SetLevel(PlayerPrefs.GetFloat(key));
    }
    public void SetLevel(float sliderValue)
    {
        Mixer.SetFloat(musicORsfx, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(key, sliderValue);
    }
}
