using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite norm, push;
    public int pushSound, releaseSound;
    public float moveSpeed;

    [Header("options")]
    public bool powerline; //if button activates a power line;
    public Color offColor, onColor;
    public bool poweredOn;
    public GameObject[] line;


    public bool openDoor;//if button opens door
    public bool activateCheck;
    public int checkNumb;
    public SeveralButtonDoor sevButtDo;
    public ScienceDoor doorToOpen;
    public int doorOpenSound;

    public bool bossLaser;//if button fires boss laser
    public BigBossLaser laser;
    public bool activateLaserCheck;
    //laser sev butt do here

    private void Start()
    {
        if (powerline)
        {
            PowerColor(offColor);
        }
    }

    private void Update()
    {
       if (openDoor)
        {
            if(doorToOpen.open != poweredOn)
            {
                doorToOpen.open = poweredOn;
            } 
        }

        if (activateCheck)
        {
            if(sevButtDo.checks[checkNumb] != poweredOn)
            {
                sevButtDo.checks[checkNumb] = poweredOn;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "Block")
        {
            theSR.sprite = push;
            AudioManager.instance.PlaySFX(pushSound);
            poweredOn = true;

            if (powerline)
            {
                PowerColor(onColor);
            }

            if (openDoor)
            {
                AudioManager.instance.PlaySFX(doorOpenSound);
            }

            if(activateCheck)
            {
                StartCoroutine(PlaySound(doorOpenSound));
            }

            if (bossLaser)
            {
                laser.Fire();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Block")
        {
            theSR.sprite = push;
            AudioManager.instance.PlaySFX(pushSound);
            poweredOn = true;

            if (powerline)
            {
                PowerColor(onColor);
            }

            if (openDoor)
            {
                AudioManager.instance.PlaySFX(doorOpenSound);
            }

            if (activateCheck)
            {
                StartCoroutine(PlaySound(doorOpenSound));
            }

            if (bossLaser)
            {
                laser.Fire();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!activateLaserCheck)
        {
            if (other.tag == "Player" || other.tag == "Block")
            {
                ButtUp();
            }
        }
       
    }

    public void ButtUp()
    {
        theSR.sprite = norm;
        AudioManager.instance.PlaySFX(releaseSound);
        poweredOn = false;

        if (powerline)
        {
            PowerColor(offColor);
        }

        if (openDoor)
        {
            AudioManager.instance.PlaySFX(doorOpenSound + 1);
        }

        if (activateCheck)
        {
            StartCoroutine(PlaySound(doorOpenSound + 1));
        }
    }

    public void PowerColor(Color col)
    {
        for(int i = 0; i < line.Length; i++)
        {
            line[i].GetComponent<SpriteRenderer>().color = col;
        }
    }

    IEnumerator PlaySound(int sund)
    {

        yield return new WaitForSeconds(0.1f);
        if (sevButtDo.multiBool)
        {
            AudioManager.instance.PlaySFX(sund);
        }
    }
}
