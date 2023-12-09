using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenGate : MonoBehaviour
{
    public int keys = 0;
    public int keysNeededToUnlock = 2;
    public Text tex;
    public GameObject textt;
    public Animator anim;
    public Collider2D colid;
    public bool openGateOnStart = false;

    private void Start()
    {
        if(keysNeededToUnlock == 1)
        {
            tex.text = "Need 1 Key to Unlock";
        }
        else
        {
            tex.text = "Need " + keysNeededToUnlock.ToString() + " Keys to Unlock";
        }

        if (openGateOnStart)
        {
            anim.SetTrigger("open");
            tex.text = "";
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            if (keys >= keysNeededToUnlock)
            {
                anim.SetTrigger("open");

            }
            else
            {
                textt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            textt.SetActive(false);
        }
    }

    public void AddKey()
    {
        keys++;
        int keysLeft = keysNeededToUnlock - keys;

        if (keysLeft == 1)
        {
            tex.text = "Need 1 Key to Unlock";
        }
        else
        {
            tex.text = "Need " + keysLeft.ToString() + " Keys to Unlock";
        }
    }

    public void DisableColid()
    {
        colid.enabled = false;
    }

    public void EnableColid()
    {
        colid.enabled = true;
    }

    public void CloseGate()
    {
        anim.SetTrigger("close");
    }
}
