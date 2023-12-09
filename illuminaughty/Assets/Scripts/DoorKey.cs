using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    [Header("technical stuff")]
    public bool key, door, closeBehind;
    public GameObject needKeyText;
    public DoorKey doorOBJ, keyOBJ, closeOBJ;
    public bool hasKey, hasOpened, openOnStart, showKeyText, selfDestruct;
    public Animator myAnim;
    public int soundToPlay;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(door && openOnStart)
        {
            myAnim.SetTrigger("EnterWithKey");
            hasOpened = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (key)
            {
                //DoorKey.instance.hasKey = true;
                doorOBJ.hasKey = true;
                AudioManager.instance.PlaySFX(soundToPlay);
                //play key pickup sound
                Destroy(gameObject);
            }

            if (door)
            {
                if (hasKey && !hasOpened)
                {
                    OpenDoor();
                    hasOpened = true;
                    showKeyText = false;
                }
                else
                {
                    if (showKeyText)
                    {
                        needKeyText.gameObject.SetActive(true);
                    }
                    
                }
            }

            if (closeBehind)
            {
                myAnim.SetTrigger("CloseBehind");
                AudioManager.instance.PlaySFX(soundToPlay);
                if (selfDestruct)
                {
                    SelfDestruct();
                }
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (door)
            {
                needKeyText.gameObject.SetActive(false);
            }
        }
    }

    public void OpenDoor()
    {
        myAnim.SetTrigger("EnterWithKey");
        AudioManager.instance.PlaySFX(soundToPlay);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    
   
}
