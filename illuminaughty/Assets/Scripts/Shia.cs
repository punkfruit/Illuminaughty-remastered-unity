using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shia : MonoBehaviour
{

    public int health;
    public int maxHealth;
    public bool active;
    public int scoreValue;

    public Animator anim;
    public SpriteRenderer body, arm1, arm2;
    public PlayerController player;

    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public string medalKey;

    public int trophyID;
    public bool gamejoltBuild = false;
    public string steamAPIname;

    public GameObject death, knockout;

    public bool knockedOut;
    public float knockOutCounter;
    private float counter;

    private Vector3 moveDirection;
    public Rigidbody2D theRB;
    public float rangeToChasePlayer, rangeToIgnorePlayer;
    public float moveSpeed;

    public bool trigger, trigger2;
    // Start is called before the first frame update
    void Start()
    {

       // player = FindObjectOfType<PlayerController>();
        health = maxHealth;
        counter = knockOutCounter;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHeightChanger();


        if (active)
        {
            if (knockedOut)
            {
                if (trigger2)
                {
                    anim.SetTrigger("stop");
                    trigger2 = false;
                }
                knockout.SetActive(true);

                moveDirection = Vector3.zero;
                if (counter > 0)
                {
                    counter -= Time.deltaTime;

                    if (counter <= 0)
                    {
                        knockedOut = false;
                        knockout.SetActive(false);

                        counter = knockOutCounter;

                        health = maxHealth;
                        UIcontroller.instance.bossHealthSlider.value = ((int)health);
                        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;

                        trigger2 = true;
                    }
                }
            }
            else
            {
                AttackPlayer();
            }
        }
        
    }

    public void AttackPlayer()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer
           && Vector3.Distance(transform.position, PlayerController.instance.transform.position) > rangeToIgnorePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();

        theRB.velocity = moveDirection * moveSpeed;

        if (trigger)
        {
            anim.SetTrigger("spin");
            trigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet" || other.tag == "Laser")
        {
            TakeDamage(other.GetComponent<PlayerBullet>().damageToGive);
        }
    }

    private void TakeDamage(int dam)
    {
        health -= dam;

        UIcontroller.instance.bossHealthSlider.value = ((int)health);
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        if (health <= 0)
        {
            //anim.SetTrigger("stop");
            knockedOut = true;
            trigger = true;
        }
    }

    public void Win()
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


        AudioManager.instance.StopAllMusic();
    }

    void PlayerHeightChanger()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (player.transform.position.y >= transform.position.y)
        {
            body.sortingLayerName = "Shooting";
            body.sortingOrder = 6;
            arm2.sortingLayerName = "Shooting";
            arm2.sortingOrder = 7;
            arm1.sortingLayerName = "Shooting";
            arm1.sortingOrder = 7;
        }
        else
        {
            body.sortingLayerName = "Player";
            body.sortingOrder = 4;
            arm2.sortingLayerName = "Player";
            arm2.sortingOrder = 5;
            arm1.sortingLayerName = "Player";
            arm1.sortingOrder = 5;
        }
    }
}
