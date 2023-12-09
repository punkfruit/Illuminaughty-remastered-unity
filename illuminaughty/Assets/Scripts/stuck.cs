using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuck : MonoBehaviour
{
    public GameObject stuckText;
    public float time, time2;
    private float counter, counter2;
    public bool inside;

    public medalUnlocker medalUN;
    // public bool unlockMedal;
    public int medalID;
    public string medalKey;
    public int trophyID;

    public bool gamejoltBuild = false;
    public string steamAPIname;

    public Collider2D colid, butcol;

    //public ScienceDoor door;
    public Button but;

    void Start()
    {
        counter = time;
        counter2 = time2;
        stuckText.SetActive(false);
    }

    void Update()
    {
        if (inside)
        {
            if (counter >= 0)
            {
                counter -= Time.deltaTime;

                if (counter <= 0)
                {
                    medalUN.unlockMedal(medalID);
                    PlayerPrefs.SetString(medalKey, "true");

                    colid.enabled = false;

                    butcol.enabled = false;
                    but.poweredOn = true;

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

            if(counter2 >= 0)
            {
                counter2 -= Time.deltaTime;

                if(counter2 <= 0)
                {
                    stuckText.SetActive(true);
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inside = false;

            counter = time;
            counter2 = time2;
        }
    }
}
