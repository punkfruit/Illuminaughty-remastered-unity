using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBoxFall : MonoBehaviour
{

    public Animator anim;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            anim.SetTrigger("boxfall");
        }
    }
}
