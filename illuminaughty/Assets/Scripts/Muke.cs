using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Muke : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.gameObject.SetActive(false);
            MouseCursor.instance.on = false;
            LevelManager.instance.canPuase = false;
            SceneManager.LoadScene(nextScene);
        }
    }
}
