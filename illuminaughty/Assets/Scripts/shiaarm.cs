using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiaarm : MonoBehaviour
{

    public int damage;
    public bool trigger = true;
    public Shia boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (trigger)
            {
                if (!boss.knockedOut)
                {
                    PlayerHealthController.instance.DamagePlayer(damage);
                    trigger = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            trigger = true;
        }
    }
}
