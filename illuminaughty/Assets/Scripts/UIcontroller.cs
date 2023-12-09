using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    public static UIcontroller instance;

    public string titleScreen, loadScreen;
    public Slider healthSlider, scoreSlider;
    public Text healthText, scoreText, levelText;

    public Image gunImage;
    public Text gunText;

    public GameObject pauseScreen;
    public GameObject optionsPanel;
    public GameObject loadingScreen;
    public GameObject youSurePanel;
    public Slider musicSlider, sfxSlider;

    public GameObject deathScreen;
    public GameObject player;

    [Header("Boss Health Bar")]
    public GameObject bossHealthOBJ;
    public Slider bossHealthSlider;
    public Text bossName, bossHealthText, outOf;

    [Header("Medal")]
    public Animator medalAnim;
    public Image ico;
    public Text medalName, value;
    public Sprite[] icons;

    public GameObject healthBar, scoreBar, pauseButton, weaponIMG;

    private void Awake()
    {
        if(instance != null)
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
        DontDestroyOnLoad(this);
        musicSlider.value = PlayerPrefs.GetFloat("MUSIC_VOLUME");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOLUME");
    }

    public void ShowMedal(string nameToUse, int valueToUse, int icon)
    {
        medalName.text = nameToUse;
        value.text = valueToUse.ToString() + " pts";
        ico.sprite = icons[icon];

        medalAnim.SetTrigger("activate");
    }

    public void Resume()
    {
        LevelManager.instance.PauseUnPause();
        optionsPanel.SetActive(false);
    }

    public void ToggleOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }



    public void Respawn()
    {
        
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        LevelManager.instance.isPaused = false;
        PlayerController.instance.gameObject.transform.position = LevelManager.instance.GetPlayerPos();
        CameraController.instance.cameraMoveSpeed = 2000f;
        AudioManager.instance.PlayMainMusic(0);
        if (bossHealthOBJ.activeInHierarchy)
        {
            bossHealthOBJ.SetActive(false);
        }
        foreach (Transform child in PlayerController.instance.gunArm)
        {
            GameObject.Destroy(child.gameObject);
        }
        PlayerController.instance.availableGuns.Clear();
        SceneManager.LoadScene(loadScreen);
    }

   public void TitleButton()
    {
        youSurePanel.SetActive(!youSurePanel.gameObject.activeInHierarchy);
    }

    public void GetAnswer(bool answer)
    {
        if (answer)
        {
            TitleButton();
            LevelManager.instance.ReturnToTitleScreen();
        }

        if (!answer)
        {
            TitleButton();
        }
    }

    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
    public void HideLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }

    public void PauseUnPausee()
    {
        LevelManager.instance.PauseUnPause();
    }


    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
