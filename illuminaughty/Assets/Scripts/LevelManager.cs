using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool isPaused, canPuase = true;
    public string titleScreen;
    public Vector3 tempPos;

    public List<EnemyController> enemiesInLevel = new List<EnemyController>();


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIcontroller.instance.gameObject.activeInHierarchy && canPuase)
            {
                PauseUnPause();
            }
        }
    }

    public void PauseUnPause()
    {
        if (!isPaused)
        {
            UIcontroller.instance.pauseScreen.SetActive(true);

            isPaused = true;
            MouseCursor.instance.on = false;
            PlayerController.instance.canMove = false;
            Time.timeScale = 0f;
        }
        else
        {
            UIcontroller.instance.pauseScreen.SetActive(false);

            isPaused = false;
            MouseCursor.instance.on = true;
            PlayerController.instance.canMove = true;
            Time.timeScale = 1f;
        }
    }

    public void LoadNextScene(string sceneToLoad)
    {
        PlayerController.instance.transform.position = GetPlayerPos();
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReturnToTitleScreen()
    {
        if(UIcontroller.instance != null)
        {
            UIcontroller.instance.SelfDestruct();
            UIcontroller.instance.pauseScreen.SetActive(false);
        }
        isPaused = false;
        if(CameraController.instance != null)
        {
            CameraController.instance.SelfDes();
        }
        PlayerController.instance.SelfDestruct();
        SceneManager.LoadScene(titleScreen);
        Time.timeScale = 1f;
        AudioManager.instance.Opening();
    }

    public void SetPlayerPos(float newX, float newY)
    {
        PlayerPrefs.SetFloat("PlayerPos_X", newX);
        PlayerPrefs.SetFloat("PlayerPos_Y", newY);
    }

    public Vector3 GetPlayerPos()
    {
       //Debug.Log(PlayerPrefs.GetFloat("PlayerPos_X") + " x " + PlayerPrefs.GetFloat("PlayerPos_Y") + " y ");
        return new Vector3(PlayerPrefs.GetFloat("PlayerPos_X"), PlayerPrefs.GetFloat("PlayerPos_Y"));
        
    }
}
