using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenaFist : MonoBehaviour
{
    public int damageToDeal, smashSound;
    public bool madeContact, turn, follow, smash;
    public CenaFist twin;

    public PlayerController player;
    public float followTimer, speed, smashSpeed, height;
    public Transform moveSpot;
    private float followCounter;
    private Vector3 playerPos;
    public Transform smashSpot;

    public float startBuffTime, endBuffTime, smashTimer, startBuffCount;
    private float endBuffCount, smashCounter;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        if (turn)
        {
            if(startBuffCount > 0)
            {
                startBuffCount -= Time.deltaTime;
                if(startBuffCount <= 0)
                {
                    follow = true;
                    followCounter = followTimer;
                }
            }
            if (follow)
            {
                playerPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                //transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);

                if(followCounter > 0)
                {
                    followCounter -= Time.deltaTime;
                    if(followCounter <= 0)
                    {
                        follow = false;
                        smash = true;
                        smashCounter = smashTimer;

                        //playerPos = new Vector3(transform.position.x, -5, transform.position.z);
                    }
                }
            }
            if (smash)
            {
               
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, smashSpot.position.y, transform.position.z), smashSpeed * Time.deltaTime);
               
                
               // if(transform.position.y == smashSpot.position.y)
                //{
               //     twin.turn = true;
               ///     twin.startBuffCount = startBuffTime;
                //    turn = false;
                //    AudioManager.instance.PlaySFX(48);
                //}
                if (smashCounter > 0)
                {
                   smashCounter -= Time.deltaTime;
                    if(smashCounter <= 0)
                   {
                        smash = false;
                        twin.turn = true;
                        twin.startBuffCount = startBuffTime;
                        turn = false;
                        AudioManager.instance.PlaySFX(48);
                    }
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        }
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!madeContact)
            {
                PlayerHealthController.instance.DamagePlayer(damageToDeal);
                madeContact = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            madeContact = false;
        }
    }
}
