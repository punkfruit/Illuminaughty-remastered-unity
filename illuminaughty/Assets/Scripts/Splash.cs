using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public string titleScreen;
    public int sound;
    // Start is called before the first frame update
    void Start()
    {
        //GoToTitle(); //just for now, in the future should be triggered by animation
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(titleScreen);
        //AudioManager.instance.constMusic = true;
    }

    public void PlayCanSound()
    {
        AudioManager.instance.PlaySFXnormal(sound);
    }
}
