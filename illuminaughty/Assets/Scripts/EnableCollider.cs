using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{
    public Collider2D colid;
  


    public void EnableColid()
    {
        colid.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            EnableColid();
        }
    }
}
