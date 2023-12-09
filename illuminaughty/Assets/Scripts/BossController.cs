using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("tech stuff")]
    public bool active = false;
    public int scoreValue;
    public int soundByte;
    public bool playSoundByte;
    public float soundWait;
    private float soundCounter;
    public Rigidbody2D theRB;
    public float rangeToChasePlayer, rangeToIgnorePlayer;
    public float rangeToAttack;
    public float rangeToShoot;
    public int RandMoveDir;
    public Transform body;
    public Transform firePoint;
    public bool generalShoot;
    public medalUnlocker medalUN;
    public bool unlockMedal;
    public int medalID;
    public string medalKey;

    public bool gamejoltBuild = false;
    public int trophyID;

    public string steamAPIname;

    [Header("Animation & Movement")]
    public bool mirrored;
    public ParticleSystem thruster1;
    public ParticleSystem thruster2;
    public bool chasePlayer;
    public bool randomMove;
    public bool followPath;
    private Vector3 moveDirection;
    public Animator anim;
    public float moveSpeed;
    public Transform[] points;
    public float moveLength;
    public float pauseLength;
    private float moveCounter, pauseCounter;

    [Header("Death & Health info")]
    public GameObject death;
    public int health = 500;
    public bool stopMusicOnDeath;
    public bool dieOnZeroHealth;
    public bool openDoorOnDeath;
    public DoorKey doorToOpen;

    [Header("Attack Player")]
    public Transform playerPos;
    public GameObject player;
    public GameObject[] attacks;
    public float coolDown;
    private float counter;

    
    void Start()
    {
        moveCounter = moveLength;
        playerPos = PlayerController.instance.transform;
        player = PlayerController.instance.gameObject;
    }

    
    void Update()
    {
        if (active && player.activeInHierarchy)
        {
            moveDirection = Vector3.zero;
            if (randomMove)
            {
                RandomMove();
            }



            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }

            jointsunFlip();
            moveDirection.Normalize();
            theRB.velocity = moveDirection * moveSpeed;
            anim.SetFloat("Left/Right", moveDirection.x);


            if (playSoundByte)
            {
                if(soundCounter > 0)
                {
                    soundCounter -= Time.deltaTime;
                }

                if(soundCounter <= 0)
                {
                    AudioManager.instance.PlaySFX(soundByte);
                    soundCounter = soundWait;
                }
            }
            /*if (playerPos.position.x > transform.position.x)
            {
                state = true;
            }
            else
            {
                state = false;
            }

            if(basee != state)
            {
                Flip();
                basee = state;
            }*/
            if (generalShoot)
            {
                if (counter > 0)
                {
                    counter -= Time.deltaTime;
                }

                if (counter <= 0)
                {
                    Instantiate(attacks[Random.Range(0, attacks.Length)], firePoint.position, firePoint.rotation);
                    counter = Random.Range(coolDown * .75f, coolDown * 1.25f);
                }
            }
        }
    }

    public void DamageBoss(int damage)
    {
        health -= damage;
        UIcontroller.instance.bossHealthSlider.value = health;
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;

        if (health <= 0)
        {
            if (stopMusicOnDeath)
            {
                AudioManager.instance.StopAllMusic();
            }
            if (dieOnZeroHealth)
            {
                Instantiate(death, transform.position, transform.rotation);
                Destroy(gameObject);
                ScoreController.instance.AddToScore(scoreValue);
            }
            if (openDoorOnDeath)
            {
                doorToOpen.OpenDoor();
            }

            if (unlockMedal)
            {
                medalUN.unlockMedal(medalID);
                PlayerPrefs.SetString(medalKey, "true");
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

            UIcontroller.instance.bossHealthOBJ.SetActive(false);
        }
    }

    public void RandomMove()
    {
        if (moveCounter > 0)
        {
            moveCounter -= Time.deltaTime;

            //move boss

            switch (RandMoveDir)
            {
                case 0:
                    moveDirection = points[0].position - transform.position;
                    break;

                case 1:
                    moveDirection = points[1].position - transform.position;
                    break;

                case 2:
                    moveDirection = points[2].position - transform.position;
                    break;

                case 3:
                    moveDirection = points[3].position - transform.position;
                    break;

                case 4:
                    moveDirection = points[4].position - transform.position;
                    break;

                case 5:
                    moveDirection = points[5].position - transform.position;
                    break;

                case 6:
                    moveDirection = points[6].position - transform.position;
                    break;

                case 7:
                    moveDirection = points[7].position - transform.position;
                    break;

                case 8:
                    moveDirection = points[8].position - transform.position;
                    break;

            }


            if (moveCounter <= 0)
            {
               
                pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f);
            }
        }

        if (pauseCounter > 0)
        {
            pauseCounter -= Time.deltaTime;

            if (pauseCounter <= 0)
            {
                moveCounter = Random.Range(moveLength * .75f, moveLength * 1.25f);

                RandMoveDir = Random.Range(0, points.Length);
            }
        }
    }

    public void ChasePlayer()
    {

    }


    public void jointsunFlip()
    {
        //make the sunglasses and weed look at player, roughly
        Vector3 playerpos = PlayerController.instance.transform.position;
        if (playerpos.x > transform.position.x)
        {
            body.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            body.localScale = Vector3.one;
        }
    }

    public  void Flip()
    {
       // if (state)
       // {
       //     body.localScale = new Vector3(-1f, 1f, 1f);
       // }
      //  else
      //  {
        //    body.localScale = Vector3.one;
      //  }
    }

    


}
