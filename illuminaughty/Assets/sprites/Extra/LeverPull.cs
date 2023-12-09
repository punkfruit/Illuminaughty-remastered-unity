using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPull : MonoBehaviour
{
    public Animator anim;
    public int soundToPlay;
    public bool inActiveRange; //if player is in the lever hitbox and can pull it

    public GameObject cubeSpawner;
    public Transform spawnLocation;

    private void Update()
    {
        if (inActiveRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("pull");
                AudioManager.instance.PlaySFX(soundToPlay);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inActiveRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inActiveRange = false;
        }
    }
    public void Pull()
    {
        Instantiate(cubeSpawner, spawnLocation.position, transform.rotation);
    }
}
