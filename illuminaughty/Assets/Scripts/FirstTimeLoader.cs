using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTimeLoader : MonoBehaviour
{
    public Slider musicSlide, sfxSlide;

    void Start()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime", 1);
        }

        if (PlayerPrefs.GetInt("firstTime") == 1)
        {
            PlayerPrefs.SetString("hasPistol", "false");
            PlayerPrefs.SetString("hasRevolvo", "false");
            PlayerPrefs.SetString("hasAK", "false");
            PlayerPrefs.SetString("hasRay", "false");
            PlayerPrefs.SetString("hasSquirt", "false");
            PlayerPrefs.SetString("hasOP", "false");
            PlayerPrefs.SetString("hasNerf", "false");

            musicSlide.value = 0.6f;
            sfxSlide.value = 0.6f;
            PlayerPrefs.SetInt("CurrentChapter", 1);
            PlayerPrefs.SetInt("CurrentMusic", 0);
            PlayerPrefs.SetFloat("MUSIC_VOLUME", musicSlide.value);
            PlayerPrefs.SetFloat("SFX_VOLUME", sfxSlide.value);
            PlayerPrefs.SetInt("SCORE", 0);
            PlayerPrefs.SetFloat("SCORE_TO_LEVEL", 100);
            PlayerPrefs.SetInt("LEVEL", 1);
            PlayerPrefs.SetInt("HEALTH", 100);
            PlayerPrefs.SetInt("MAX_HEALTH", 100);
            PlayerPrefs.SetString("LAST_SCENE", "test");

            PlayerPrefs.SetFloat("PlayerPos_X", 0f);
            PlayerPrefs.SetFloat("PlayerPos_Y", 0f);

            //enemies encountered
            PlayerPrefs.SetString("BasicCan", "false");
            PlayerPrefs.SetString("BasicBag", "false");
            PlayerPrefs.SetString("LiveWireCan", "false");
            PlayerPrefs.SetString("VoltageCan", "false");
            PlayerPrefs.SetString("CoolRanchBag", "false");
            PlayerPrefs.SetString("WhiteOutCan", "false");
            PlayerPrefs.SetString("SpicyNachoBag", "false");
            PlayerPrefs.SetString("CodeRedCan", "false");
            PlayerPrefs.SetString("BajaBlastCan", "false");
            PlayerPrefs.SetString("PitchBlackCan", "false");

            //medals unlocked
            PlayerPrefs.SetString("Chap1Med", "false");
            PlayerPrefs.SetString("Chap2Med", "false");
            PlayerPrefs.SetString("Chap3Med", "false");
            PlayerPrefs.SetString("Chap4Med", "false");
            PlayerPrefs.SetString("Chap5Med", "false");
            PlayerPrefs.SetString("Chap6Med", "false");

            PlayerPrefs.SetString("final", "false");

            PlayerPrefs.SetString("BroomCloset", "false");
            PlayerPrefs.SetString("stuck", "false");

            // PlayerPrefs.SetInt("firstTime", 0); //turns first time off

            PlayerPrefs.SetString("CHECKPOINT", "false");
        }

        PlayerPrefs.SetString("SeenSummonBoss1", "false");
        PlayerPrefs.SetString("SeenSummonBoss2", "false");
        PlayerPrefs.SetString("SeenSummonBoss3", "false");
        PlayerPrefs.SetString("SeenSummonBoss4", "false");
        PlayerPrefs.SetString("SeenSummonBoss5", "false");
        PlayerPrefs.SetString("SeenSummonBoss6", "false");
    }
}
