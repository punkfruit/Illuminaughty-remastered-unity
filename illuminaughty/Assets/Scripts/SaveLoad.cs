using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;

    public string chapter1;
    public Gun[] guns;

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
    // Start is called before the first frame update
    void Start()
    {
       // Load();

    }

    public void Load()
    {

        PlayerHealthController.instance.maxHealth = PlayerPrefs.GetInt("MAX_HEALTH");
        PlayerHealthController.instance.currentHealth = PlayerPrefs.GetInt("HEALTH");
        

        ScoreController.instance.score = PlayerPrefs.GetInt("SCORE");

        ScoreController.instance.level = PlayerPrefs.GetInt("LEVEL");
        ScoreController.instance.scoreToLevel = PlayerPrefs.GetFloat("SCORE_TO_LEVEL");

        if (PlayerPrefs.GetString("hasPistol") == "true")
        {
            LoadGun(0);
        }

        if (PlayerPrefs.GetString("hasRevolvo") == "true")
        {
            LoadGun(1);
        }

        if (PlayerPrefs.GetString("hasRay") == "true")
        {
            LoadGun(3);
        }

        if (PlayerPrefs.GetString("hasAK") == "true")
        {
            LoadGun(2);
        }


        if (PlayerPrefs.GetString("hasSquirt") == "true")
        {
            LoadGun(4);
        }

        if (PlayerPrefs.GetString("hasOP") == "true")
        {
            LoadGun(5);
        }

        if (PlayerPrefs.GetString("hasNerf") == "true")
        {
            LoadGun(6);
        }

        //PlayerController.instance.currentGun = PlayerPrefs.GetInt("gunNumb");
        PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
        PlayerController.instance.SwitchGun();

        PlayerHealthController.instance.Initialize();
        ScoreController.instance.Initialize();
        PlayerController.instance.transform.position = LevelManager.instance.GetPlayerPos();
        if(PlayerPrefs.GetInt("CurrentMusic") != AudioManager.instance.playingMusic)
        {
            AudioManager.instance.PlayMainMusic(PlayerPrefs.GetInt("CurrentMusic"));
        }
        PlayerPrefs.Save();
    }

    public void LoadGun(int gunToLoad)
    {
        Gun gunClone = Instantiate(guns[gunToLoad]);
        gunClone.transform.parent = PlayerController.instance.gunArm;
        gunClone.transform.position = PlayerController.instance.gunArm.position;
        gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gunClone.transform.localScale = Vector3.one;

        PlayerController.instance.availableGuns.Add(gunClone);
    }

    public void StartNew()
    {
        PlayerPrefs.SetString("hasPistol", "false");
        PlayerPrefs.SetString("hasRevolvo", "false");
        PlayerPrefs.SetString("hasAK", "false");
        PlayerPrefs.SetString("hasRay", "false");
        PlayerPrefs.SetString("hasSquirt", "false");
        PlayerPrefs.SetString("hasOP", "false");
        PlayerPrefs.SetString("hasNerf", "false");
        PlayerPrefs.SetInt("SCORE", 0);
        PlayerPrefs.SetFloat("SCORE_TO_LEVEL", 100);
        PlayerPrefs.SetInt("LEVEL", 1);
        PlayerPrefs.SetInt("HEALTH", 100);
        PlayerPrefs.SetInt("MAX_HEALTH", 100);
        PlayerPrefs.SetString("LAST_SCENE", chapter1);
        LevelManager.instance.SetPlayerPos(-5f, 0f);
        PlayerPrefs.SetInt("CurrentMusic", 0);
        PlayerPrefs.Save();
        Load();
    }

    public void Save(string chapterLevelID)
    {
       
        PlayerPrefs.SetInt("SCORE", ScoreController.instance.score);
        PlayerPrefs.SetInt("LEVEL", ScoreController.instance.level);
        PlayerPrefs.SetFloat("SCORE_TO_LEVEL", ScoreController.instance.scoreToLevel);
        PlayerPrefs.SetInt("HEALTH", PlayerHealthController.instance.currentHealth);
        PlayerPrefs.SetInt("MAX_HEALTH", PlayerHealthController.instance.maxHealth);
        PlayerPrefs.SetString("LAST_SCENE", chapterLevelID);
        PlayerPrefs.Save();
    }

   
}
