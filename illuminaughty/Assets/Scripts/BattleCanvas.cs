using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleCanvas : MonoBehaviour
{
    public string titleScreen, loadScreen, whatWillPlayerDo = "What will you do?";
    public GameObject[] panels;
    public GameObject raycastBlock, enemyHealth;
    public Text announceText, playerHealthText, gunInfoText, playerLevelLabel, playerLevelText;

    public Chap5Boss[] bosses;
    public int selectedBoss, currentTurn = 0;

    public int top = 166, mid = 180, bot = 191;
    public string currentGun;

    public float counter1Time, bufferTimer;
    private float counter1, bufferCounter;

    public float gunTime;
    private float gunCounter;
    public int shotAmount;

    public float enemyHealthTimer;
    private float enemyHealthCounter;

    public float cameraSpeedBuffer;
    private float cameraSpeedCounter;

    public int deadBosses;

    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public string medalKey;
    public bool gamejoltBuild;
    public string steamAPIname;

    public int trophyID;
    //public PlayerHealthController phc;
    //public ScoreController sc;
    // Start is called before the first frame update
    void Start()
    {
        TextWritter.instance.AddWriter(announceText, "A Random DirtGirl Appeared!", .1f);

        playerHealthText.text = PlayerHealthController.instance.currentHealth.ToString() + " / " + PlayerHealthController.instance.maxHealth.ToString();
        playerLevelLabel.text = "Level: " + ScoreController.instance.level;
        playerLevelText.text = ScoreController.instance.displayScore.ToString() + " / " + ScoreController.instance.scoreToLevel;

        counter1 = counter1Time;

    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = PlayerHealthController.instance.currentHealth.ToString() + " / " + PlayerHealthController.instance.maxHealth.ToString();
        playerLevelLabel.text = "Level: " + ScoreController.instance.level;
        playerLevelText.text = ScoreController.instance.displayScore.ToString() + " / " + ScoreController.instance.scoreToLevel.ToString("F0");

        if (gameObject.activeInHierarchy)
        {

            //raycastBlock.SetActive(!bosses[0].myTurn);
            if(counter1 > 0)
            {
                counter1 -= Time.deltaTime;
                if(counter1 <= 0)
                {
                    TextWritter.instance.AddWriter(announceText, whatWillPlayerDo, .1f);
                }
            }



            if(shotAmount > 0)
            {
                if(gunCounter > 0)
                {
                    gunCounter -= Time.deltaTime;
                    if(gunCounter <= 0)
                    {
                        PlayerController.instance.availableGuns[PlayerController.instance.currentGun].Fire();
                        shotAmount--;

                        if(shotAmount > 0)
                        {
                            gunCounter = gunTime;
                        }else
                        {
                            //ChangeTurn();
                            bufferCounter = bufferTimer;
                        }
                        
                    }
                }
            }

            if(bufferCounter > 0)
            {
                bufferCounter -= Time.deltaTime;
                if(bufferCounter <= 0)
                {
                    ChangeTurn();
                }
            }

            if(enemyHealthCounter > 0)
            {
                enemyHealthCounter -= Time.deltaTime;
                if(enemyHealthCounter <= 0)
                {
                    enemyHealth.SetActive(false);
                }
            }

            if(cameraSpeedCounter > 0)
            {
                cameraSpeedCounter -= Time.deltaTime;
                if(cameraSpeedCounter <= 0)
                {
                    CameraController.instance.cameraMoveSpeed = 2000f;
                }
            }
        }
    }

    public void ChangeTurn()
    {
        currentTurn++;
        if(currentTurn >= bosses.Length) { currentTurn = 0; }

        for(int i = 0; i < bosses.Length; i++)
        {
            bosses[i].myTurn = false;
        }

        bosses[currentTurn].myTurnTrigger = true;
    }


    public void CancelButton(int pann)
    {
        panels[pann].SetActive(false);
    }

    public void ShowPanel(int pane)
    {
        panels[pane].SetActive(true);
    }

    public void ChooseGun(string gunToChoose)
    {
        PlayerController.instance.SwitchGunWithString(gunToChoose);

        switch (gunToChoose)
        {
            case "Pistol":
                TextWritter.instance.AddWriter(gunInfoText, "Selected: pistol - 4 shots per turn", .02f);
                break;
            case "   Shotty":
                TextWritter.instance.AddWriter(gunInfoText, "Selected: shotty - 2 shots per turn - spread", .02f);
                break;
            case "Chris":
                TextWritter.instance.AddWriter(gunInfoText, "Selected: raygun - 2 shots per turn", .02f);
                break;
            case "AK-47 - JetSet":
                TextWritter.instance.AddWriter(gunInfoText, "Selected: AK-47 - 5 shots per turn", .02f);
                break;
            case "Dew Dealer":
                TextWritter.instance.AddWriter(gunInfoText, "Selected: squirt gun - 2 shots per turn", .02f);
                break;
            case "      AVP":
                TextWritter.instance.AddWriter(gunInfoText, "Selected: AVP - 1 shot per turn", .02f);
                break;
        }

        currentGun = gunToChoose;
    }

    public void chooseEnemy(int bossToChoose)
    {
        selectedBoss = bossToChoose;
        ShowPanel(3);
        CancelButton(5);
    }


    public void TempGunArmRotate(int degree)
    {
        PlayerController.instance.transform.localScale = new Vector3(-1f, 1f, 1f);
        PlayerController.instance.gunArm.rotation = Quaternion.Euler(0, 0, 0);
        PlayerController.instance.gunArm.localScale = new Vector3(-1f, -1f, 1f);


        PlayerController.instance.gunArm.transform.eulerAngles = new Vector3(
    PlayerController.instance.gunArm.transform.eulerAngles.x,
    PlayerController.instance.gunArm.transform.eulerAngles.y,
    PlayerController.instance.gunArm.transform.eulerAngles.z + degree );
    }

    private void OnEnable()
    {
        TempGunArmRotate(mid);
        MouseCursor.instance.on = false;
        bosses[currentTurn].myTurn = true;
        UpdateGun();
    }

    public void LoadCheckpoint()
    {
        PlayerController.instance.gameObject.transform.position = LevelManager.instance.GetPlayerPos();
        AudioManager.instance.PlayMainMusic(0);
        foreach (Transform child in PlayerController.instance.gunArm)
        {
            GameObject.Destroy(child.gameObject);
        }
        PlayerController.instance.availableGuns.Clear();

        UIcontroller.instance.gameObject.SetActive(true);
        PlayerController.instance.canMove = true;
        CameraController.instance.cameraMoveSpeed = 2000f;
        CameraController.instance.SetGetCameraFollowPositionFunc(() => PlayerController.instance.cameraTarget.position);
        SceneManager.LoadScene(loadScreen);
    }

    public void Attack()
    {
        //PlayerController.instance.availableGuns[PlayerController.instance.currentGun].Fire();

        switch (currentGun)
        {
            case "Pistol":
                TextWritter.instance.AddWriter(announceText, "Player used Pistol!", .1f);
                shotAmount = 4;
                gunCounter = gunTime;
                break;
            case "   Shotty":
                TextWritter.instance.AddWriter(announceText, "Player used Shotty!", .1f);
                shotAmount = 2;
                gunCounter = gunTime;
                break;
            case "Chris":
                TextWritter.instance.AddWriter(announceText, "Player used Chris!", .1f);
                shotAmount = 2;
                gunCounter = gunTime;
                break;
            case "AK-47 - JetSet":
                TextWritter.instance.AddWriter(announceText, "Player used AK-47 JetSet!", .1f);
                shotAmount = 5;
                gunCounter = gunTime;
                break;
            case "Dew Dealer":
                TextWritter.instance.AddWriter(announceText, "Player used Dew Dealer!", .1f);
                shotAmount = 2;
                gunCounter = gunTime;
                break;
            case "      AVP":
                TextWritter.instance.AddWriter(announceText, "Player used AVP!", .1f);
                shotAmount = 1;
                gunCounter = gunTime;
                break;
        }

        CancelButton(3);
        raycastBlock.SetActive(true);
    }

    public void Skill(int skl)
    {

        switch (skl)
        {
            case 0:
                enemyHealth.SetActive(true);
                enemyHealthCounter = enemyHealthTimer;
                TextWritter.instance.AddWriter(announceText, "Player used a keen eye to spot the health of the enemy", .03f);
                break;
            case 1:
                int healam = Random.Range(40, 80);
                PlayerHealthController.instance.HealPlayer(healam);
                TextWritter.instance.AddWriter(announceText, "Player healed themselves for " + healam + " points!", .03f);
                break;
        }
        CancelButton(4);
        raycastBlock.SetActive(true);
        bufferCounter = bufferTimer + 2;
        //show enemy health, heal self,
    }

    public void textTest()
    {
        /*
        TextWritter.instance.AddWriter(announceText, "this is a test lmao", .1f);
        ScoreController.instance.AddToScore(50);
        counter1 = 4;
        //TempGunArmRotate(mid);
        ChooseGun(PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunName);
        TextWritter.instance.AddWriter(announceText, whatWillPlayerDo, .1f);
        */

        ChangeTurn();
    }

    public void UpdateGun()
    {
        switch (PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunName)
        {
            case "Pistol":
                gunInfoText.text = "Selected: pistol - 4 shots per turn";
                //TextWritter.instance.AddWriter(gunInfoText, "Selected: pistol - 4 shots per turn", .02f);
                break;
            case "   Shotty":
                gunInfoText.text = "Selected: shotty - 2 shots per turn - spread";
                //TextWritter.instance.AddWriter(gunInfoText, "Selected: shotty - 2 shots per turn - spread", .02f);
                break;
            case "Chris":
                gunInfoText.text = "Selected: raygun - 2 shots per turn";
                //TextWritter.instance.AddWriter(gunInfoText, "Selected: raygun - 2 shots per turn", .02f);
                break;
            case "AK-47 - JetSet":
                gunInfoText.text = "Selected: AK-47 - 5 shots per turn";
                //TextWritter.instance.AddWriter(gunInfoText, "Selected: AK-47 - 14 shots per turn", .02f);
                break;
            case "Dew Dealer":
                gunInfoText.text = "Selected: squirt gun - 2 shots per turn";
                //TextWritter.instance.AddWriter(gunInfoText, "Selected: squirt gun - 2 shots per turn", .02f);
                break;
            case "      AVP":
                gunInfoText.text = "Selected: AVP - 1 shot per turn";
                //TextWritter.instance.AddWriter(gunInfoText, "Selected: AVP - 1 shot per turn", .02f);
                break;
        }

        currentGun = PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunName;
    }

    public void LoadTitlescreen()
    {
        LevelManager.instance.ReturnToTitleScreen();
    }

    public void BossKilled()
    {
        deadBosses++;
        if(deadBosses >= 3)
        {
            //win
            AudioManager.instance.StopAllMusic();
            UIcontroller.instance.gameObject.SetActive(true);
            PlayerController.instance.canMove = true;
            CameraController.instance.cameraMoveSpeed = 2000f;
            //cameraSpeedCounter = cameraSpeedBuffer;
            CameraController.instance.SetGetCameraFollowPositionFunc(() => PlayerController.instance.cameraTarget.position);
            MouseCursor.instance.on = true;
            MouseCursor.instance.cursor.color = MouseCursor.instance.onn;
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

            gameObject.SetActive(false);
        }
    }
}
