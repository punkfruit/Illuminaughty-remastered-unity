using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContact : MonoBehaviour
{
    bool onPlayer = false;
    bool damageDealt = false;
    public int soundToPlay;

    public int damageToGive;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(soundToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer()
    {
        if (onPlayer)
        {
            PlayerHealthController.instance.DamagePlayer(damageToGive);
            damageDealt = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !damageDealt)
        {
            onPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            onPlayer = false;
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
