using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public int musicNumb = 8;
    public Transform cameraMoveZone;
    public string title;

    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public string medalKey;

    public int trophyID;

    public bool gamejoltBuild;
    public string steamAPIname;
    public CameraChanger camChan;
    // Start is called before the first frame update
    void Start()
    {
        //CameraController.instance.cameraMoveSpeed = 2f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => cameraMoveZone.position);
        MouseCursor.instance.on = true;

        UIcontroller.instance.healthBar.SetActive(false);
        UIcontroller.instance.scoreBar.SetActive(false);
        UIcontroller.instance.pauseButton.SetActive(false);
        UIcontroller.instance.weaponIMG.SetActive(false);
        UIcontroller.instance.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMusic()
    {
        AudioManager.instance.PlayMainMusic(musicNumb);
    }

    public void goToTitle()
    {
        camChan.backToNorm();
        //SceneManager.LoadScene(title);
        LevelManager.instance.canPuase = true;
        LevelManager.instance.ReturnToTitleScreen();
        MouseCursor.instance.on = false;
        //Destroy(MouseCursor.instance.gameObject);
    }

    public void TurnOffCursor()
    {
        MouseCursor.instance.on = false;
    }

    public void unlockkMedal()
    {
        medalUN.unlockMedal(medalID);
        PlayerPrefs.SetString(medalKey, "true");

        if (gamejoltBuild)
        {
            GameJolt.API.Trophies.Unlock(trophyID, (bool success) => {
                if (success)
                {
                    Debug.Log("Success!");
                }
                else
                {
                    Debug.Log("Something went wrong");
                }
            });
        }

        if (SteamManager.Initialized)
        {
            Steamworks.SteamUserStats.SetAchievement(steamAPIname);
            Steamworks.SteamUserStats.StoreStats();
            Debug.Log("Achievement " + steamAPIname + " unlocked");
        }
    }
}
