using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOADER : MonoBehaviour
{
    public bool newGame;
    // Start is called before the first frame update
    void Start()
    {
        UIcontroller.instance.loadingScreen.SetActive(true);
        if (newGame)
        {
            PlayerController.instance.canMove = false;
            SaveLoad.instance.StartNew();
            StartCoroutine(wait());
            PlayerPrefs.SetInt("firstTime", 0);
        }

        if (!newGame)
        {
            PlayerController.instance.canMove = false;
            SaveLoad.instance.Load();
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);

        PlayerController.instance.canMove = true;
        SceneManager.LoadScene(PlayerPrefs.GetString("LAST_SCENE"));
    }

   
}
