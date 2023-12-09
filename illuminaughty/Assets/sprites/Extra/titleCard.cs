using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleCard : MonoBehaviour
{
    public string NextScene;
    // Start is called before the first frame update
    public SpriteRenderer theSR;
    public void Inception()
    {
        AudioManager.instance.PlaySFXnormal(0);
    }

    public void SelfDestruct()
    {
        theSR.enabled = false;
        AudioManager.instance.Opening();
    }

    public void StartSpeech()
    {
        AudioManager.instance.PlaySFXnormal(18);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
