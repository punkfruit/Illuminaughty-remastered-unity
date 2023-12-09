using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleCanvas : MonoBehaviour
{
    public GameObject continueButton, arcadeButton;
    public GameObject optionsScreen, playPanel, youSurePanel;
    public GameObject[] BackGrounds;
    public Image whitePanel;
    public Slider musicSlider, sfxSlider;
    public string newGameScene, continueLoadScene, arcadeScene, aboutScene;
    public Animator anim;
    
    void Start()
    {

        //AudioManager.instance.constMusic = true;
        
        if(PlayerPrefs.GetInt("firstTime") == 1)
        {
            continueButton.gameObject.SetActive(false);
            arcadeButton.gameObject.SetActive(false);
            AudioManager.instance.Opening();
        }
        else
        {
            AudioManager.instance.PlayMainMusic(0);
        }
        musicSlider.value = PlayerPrefs.GetFloat("MUSIC_VOLUME");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOLUME");
        StartCoroutine("WaitOpening");

        switch (PlayerPrefs.GetInt("CurrentChapter"))
        {
            case 1:
                //make background white
                whitePanel.enabled = true;
                break;

            default:
                whitePanel.enabled = true;
                break;
        }


        /*if (!GeneralManager.instance.initialized)
        {
            AudioManager.instance.Opening();
            GeneralManager.instance.initialized = true;
        }*/

        /*if(PlayerPrefs.GetInt("CurrentMusic") == 0)
        {
            AudioManager.instance.Opening();
        }
        else
        {
            AudioManager.instance.PlayMainMusic(PlayerPrefs.GetInt("CurrentMusic"));
        }*/
        CameraManager.instance.active = false;
    }

   public void LoadAboutScene()
    {
        SceneManager.LoadScene(aboutScene);
    }

    public void ToggleOptionsPanel()
    {
        optionsScreen.SetActive(!optionsScreen.activeInHierarchy);
    }

    public void ShowPlayPanel(bool state)
    {
        playPanel.gameObject.SetActive(state);
    }

   public void YouSurePanel(bool state)
    {
        youSurePanel.gameObject.SetActive(state);
    }

   

    public void StartNewGame()
    {
        SceneManager.LoadScene(newGameScene);
        PlayerPrefs.SetString("CHECKPOINT", "false");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueLoadScene);
    }

    IEnumerator WaitOpening()
    {
        
        yield return new WaitForSeconds(0.1f);
        if (PlayerPrefs.GetInt("firstTime") == 1)
        {
            //continueButton.gameObject.SetActive(false);
           // arcadeButton.gameObject.SetActive(false);
            AudioManager.instance.Opening();
        }
        else
        {
            //AudioManager.instance.PlayMainMusic(0);
            AudioManager.instance.constMusic = true;
        }
       // CameraManager.instance.active = true;
    }

    public void CheckAnim()
    {
        if (GeneralManager.instance.animationDone)
        {
            anim.enabled = false;
        }
    }

    public void DisableAnim()
    {
        anim.enabled = false;
        GeneralManager.instance.animationDone = true;
    }

    public void quitApp()
    {
        Application.Quit();
    }
}
