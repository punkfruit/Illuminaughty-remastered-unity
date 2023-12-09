using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChapterStarter : MonoBehaviour
{
    //public Transform playerPosition; //makes player start here
    public string levelName; //saves last level
    public bool setPlayerPos; // if you want to set the level manager player pos variable. seel line 23
    public bool movePlayerToPos = true;
    public Vector3 playerPosToSet; // ^^^
    public bool setTempPos;
    public Vector3 tempPosToSet;
    public bool changeMusic; //sets destroyBulletsOnBecomeInvis in general manager to match this bool
    public int musicToSet, currentChapter;
    // Start is called before the first frame update
    void Start()
    {
        UIcontroller.instance.loadingScreen.SetActive(false);

        SaveLoad.instance.Save(levelName);

        //GeneralManager.instance.checkpointReached = false;

        if (setPlayerPos)
        {
            LevelManager.instance.SetPlayerPos(playerPosToSet.x, playerPosToSet.y); // sets this position for the beggining of the chapter
            //LevelManager.instance.playerPos = playerPosToSet;
        }
        if (movePlayerToPos)
        {
            PlayerController.instance.transform.position = LevelManager.instance.GetPlayerPos();
        }

        if (setTempPos)
        {
            LevelManager.instance.tempPos = tempPosToSet;
        }

        if (changeMusic && musicToSet != PlayerPrefs.GetInt("CurrentMusic")) 
        {
            StartCoroutine("MusicChange");
        }

        PlayerPrefs.SetInt("CurrentChapter", currentChapter);

        PlayerPrefs.Save();

        //GeneralManager.instance.destroyBulletsOnBecomeInvis = changeBullletIvis;

        if (!PlayerController.instance.gameObject.activeInHierarchy)
        {
            PlayerController.instance.gameObject.SetActive(true);
        }
    }

    IEnumerator MusicChange()
    {

        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.PlayMainMusic(musicToSet);
        PlayerPrefs.SetInt("CurrentMusic", musicToSet);


    }
}
