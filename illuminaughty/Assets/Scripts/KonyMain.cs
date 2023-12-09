using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonyMain : MonoBehaviour
{
    private PlayerController player;
    public float buffer;
    public bool active;

    public Sprite[] main;
    public Sprite[] mouths;
    public SpriteRenderer mainn, mouth;
    public GameObject death;
    public Animator anim;
    public BigBossLaser laser;

    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public int trophyID;
    public bool gamejoltBuild = false;
    public string steamAPIname;

    public int scoreValue = 1000;

    // Start is called before the first frame update
    void Start()
    {
        if (anim.enabled)
        {
            anim.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }


        if (active)
        {
            if (player.transform.position.x > transform.position.x + buffer)
            {
                mainn.sprite = main[0];
                mouth.sprite = mouths[0];
            }
            else if (player.transform.position.x < transform.position.x - buffer)
            {
                mainn.sprite = main[2];
                mouth.sprite = mouths[2];
            }
            else
            {
                mainn.sprite = main[1];
                mouth.sprite = mouths[1];
            }
        }
    }

    public void FaceForward()
    {
        mainn.sprite = main[1];
        mouth.sprite = mouths[1];
    }

    public void Die()
    {
        Instantiate(death, transform.position, transform.rotation);
        if (unlockMedal)
        {
            medalUN.unlockMedal(medalID);
            PlayerPrefs.SetString("Chap2Med", "true");
            ScoreController.instance.AddToScore(scoreValue);
        }

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

    public void MakeHol()
    {
        laser.MakeHole();
    }

    public void TrexRoar()
    {
        AudioManager.instance.PlaySFX(34);
    }
}
