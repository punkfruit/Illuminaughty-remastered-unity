using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vita : MonoBehaviour
{
    public int spinSound, spinConfirmSound;
    public int scoreValue;

    public bool on, active;
    public float timeBetweenSpins, timeBetweenGroups, baseSpinTime, spinIncrement, attackLength, speed;
    private float counter, counter2, spinTimer, spinTimeSurrogate;
    private int spinCounter, screenIndex;
    public int maxSpins, minSpins, health, maxHealth;
    public bool spin = false;
    public Transform center;

    public SpriteRenderer screen;
    public Sprite[] screens;
    public Sprite blankScreen;
    public GameObject[] shots;

    public PlayerController player;


    public bool trigger, trigger2;
    private Vector3 playerPos;

    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public string medalKey;
    public bool gamejoltBuild = false;
    public string steamAPIname;

    public int trophyID;

    public GameObject death;
    public SpriteRenderer newWalkWay;//bridge thing that appears after vita is killed
    public BoxCollider2D oldCollid; // collider to remove after vita is killed
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        spinCounter = 0;
        screen.sprite = blankScreen;
        spinTimer = baseSpinTime;
        spinTimeSurrogate = baseSpinTime;
        counter = 0.4f;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       //// if(player = null)
       // {
            player = FindObjectOfType<PlayerController>();
        //}

        if (active && player != null)
        {

            if (!trigger)
            {
                if (counter > 0)//timeBetweenSpins
                {
                    counter -= Time.deltaTime;
                    if (counter <= 0)
                    {
                        StartSpin();
                        trigger = true;
                    }
                }
            }
            

            if (spin)
            {
                playerPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);

                if (spinCounter > 0)
                {
                    spinTimer -= Time.deltaTime;
                    if (spinTimer <= 0)
                    {
                        AudioManager.instance.PlaySFX(spinSound);

                        screenIndex++;
                        if (screenIndex >= screens.Length) { screenIndex = 0; }
                        screen.sprite = screens[screenIndex];

                        spinCounter -= 1;
                        spinTimeSurrogate += spinIncrement / spinCounter;
                        spinTimer = spinTimeSurrogate;
                    }
                }
                else if (spinCounter <= 0)
                {
                    spin = false;
                    AudioManager.instance.PlaySFXnormal(spinConfirmSound);
                    counter2 = attackLength;
                    trigger2 = true;

                    
                }
            }

            if(counter2 > 0 && counter <= 0)
            {
                counter2 -= Time.deltaTime;

                if(counter2 <= attackLength / 2 && trigger2)
                {
                    Instantiate(shots[screenIndex], center);
                    trigger2 = false;
                }


                if(counter2 <= 0)
                {
                    
                    counter = timeBetweenSpins;
                    screen.sprite = blankScreen;
                    trigger = false;
                }
            }
        }

    }

    public void StartSpin()
    {
        AudioManager.instance.PlaySFX(spinSound);
        spinCounter = Random.Range(minSpins, maxSpins);
        spinTimer = baseSpinTime;
        spinTimeSurrogate = baseSpinTime;
        screenIndex = 0;
        screen.sprite = screens[screenIndex];
        spin = true;
    }

    private void TakeDamage(int dam)
    {
        health -= dam;

        UIcontroller.instance.bossHealthSlider.value = ((int)health);
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        if (health <= 0)
        {
            //die
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

            UIcontroller.instance.bossHealthOBJ.SetActive(false);

            active = false;

            Instantiate(death, transform.position, transform.rotation);
            Destroy(gameObject);
            ScoreController.instance.AddToScore(scoreValue);

            oldCollid.enabled = false;
            newWalkWay.enabled = true;

            AudioManager.instance.StopAllMusic();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet" || other.tag == "Laser")
        {
            TakeDamage(other.GetComponent<PlayerBullet>().damageToGive);
        }
    }
}
