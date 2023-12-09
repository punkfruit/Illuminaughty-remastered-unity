using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShoy : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite off, on;
    public bool poweredOn;

    public bool openDoor;
    public ScienceDoor doorToOpen;
    public int doorOpenSound;
    public int powerSND;

    public bool activateCheck;
    public SeveralButtonDoor sevButtDo;
    public int checkNumb;


    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            if (doorToOpen.open != poweredOn)
            {
                doorToOpen.open = poweredOn;
            }
        }

        if (activateCheck)
        {
            if (sevButtDo.checks[checkNumb] != poweredOn)
            {
                sevButtDo.checks[checkNumb] = poweredOn;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            poweredOn = true;
            theSR.sprite = on;

            AudioManager.instance.PlaySFX(powerSND);

            if (openDoor)
            {
                AudioManager.instance.PlaySFX(doorOpenSound);
            }
        }
    }
}
