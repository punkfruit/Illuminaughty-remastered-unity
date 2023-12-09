using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomCloset : MonoBehaviour
{

    public float time;
    private float counter;
    public bool inside;

    public medalUnlocker medalUN;
   // public bool unlockMedal;
    public int medalID;
    public string medalKey;
    public int trophyID;
    public bool gamejoltBuild = false;
    public string steamAPIname;

    public Collider2D colid;
    // Start is called before the first frame update
    void Start()
    {
        counter = time;
    }

    // Update is called once per frame
    void Update()
    {
        if(inside)
        {
            if(counter >= 0)
            {
                counter -= Time.deltaTime;

                if(counter <= 0)
                {
                    medalUN.unlockMedal(medalID);
                    PlayerPrefs.SetString(medalKey, "true");

                    colid.enabled = false;

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
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
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
        }
    }
}
