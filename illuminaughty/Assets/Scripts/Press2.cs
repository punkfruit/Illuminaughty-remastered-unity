using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Press2 : MonoBehaviour
{
   

    public void GotoNextScene(string scenee)
    {
        SceneManager.LoadScene(scenee);
       // PlayerController.instance.availableGuns.Clear();
        PlayerController.instance.gameObject.SetActive(false);
    }

    public void GotoNextScene2(string scenee)
    {
        SceneManager.LoadScene(scenee);
        //PlayerController.instance.gameObject.SetActive(false);
    }
}
