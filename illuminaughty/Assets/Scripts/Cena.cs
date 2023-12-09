using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cena : MonoBehaviour
{
    public CenaFist fist1, fist2;
    public int health, maxHealth;
    public BossSummon6 bossum;
    public Collider2D collid;
    public GameObject path, death;

    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public string medalKey;

    public bool gamejoltBuild = false;
    public string steamAPIname;

    public int trophyID;
    // Start is called before the first frame update
    void Start()
    {
        health = bossum.bossHealth;
        maxHealth = bossum.bossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" || other.tag == "Laser")
        {
            TakeDamage(other.GetComponent<PlayerBullet>().damageToGive);
        }
    }

    private void TakeDamage(int damageToGive)
    {
        health -= damageToGive;
        UIcontroller.instance.bossHealthSlider.value = ((int)health);
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        if (health <= 0)
        {
            //parent.active = false;
            fist1.turn = false;
            fist2.turn = false;
            AudioManager.instance.StopAllMusic();
            //parent.anim.enabled = true;
            collid.enabled = false;
            UIcontroller.instance.bossHealthOBJ.SetActive(false);
            path.SetActive(true);
            Instantiate(death, transform.position, transform.rotation);
            Destroy(gameObject);

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


            //die
        }
    }
}
