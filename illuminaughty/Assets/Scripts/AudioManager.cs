using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioMixer MainMixer;
    public AudioSource[] music;
    public int playingMusic;
    public bool constMusic;

    public AudioSource[] SFX;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(instance == null)
        {
            instance = this;
        }

        if (constMusic)
        {
            if (!music[playingMusic].isPlaying)
            {
                PlayMainMusic(playingMusic);
            }
        }
    }

    public void PlayMainMusic(int musicToPlay)
    {
        constMusic = true;
        music[playingMusic].Stop();
        music[musicToPlay].Stop();
        music[musicToPlay].Play();
        playingMusic = musicToPlay;
        
    }

    public void StopAllMusic()
    {
        constMusic = false;
        music[playingMusic].Stop();
    }

    public void PlaySFX(int soundToPlay)
    {
        SFX[soundToPlay].Stop();
        SFX[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        SFX[soundToPlay].Play();
    }

    public void PlaySFXnormal(int soundToPlay)
    {
        SFX[soundToPlay].Stop();
        SFX[soundToPlay].Play();
    }

    public void StopSFX(int soundToStop)
    {
        SFX[soundToStop].Stop();
    }

    public void Opening()
    {
        StartCoroutine("WaitOpening");
    }

    

    IEnumerator WaitOpening()
    {
        PlayMainMusic(1);
        yield return new WaitForSeconds(1.9f);
        PlayMainMusic(0);
        
    }

    public void StopOpening()
    {
        StopCoroutine("WaitOpening");
    }
}
