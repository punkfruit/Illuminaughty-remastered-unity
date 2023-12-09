using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chapZeroTextFade : MonoBehaviour
{
    public string sceneToLoad;

    private void Start()
    {
        UIcontroller.instance.gameObject.SetActive(false);
        PlayerController.instance.gameObject.SetActive(false);
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
