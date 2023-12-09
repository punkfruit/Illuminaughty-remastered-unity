using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToGive;
    public int medxSND, stimSND;
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
            if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.HealPlayer(healthToGive);

                int soundd = Random.Range(0, 100);

                if(soundd >= 50)
                {
                    AudioManager.instance.PlaySFX(medxSND);
                }
                else
                {
                    AudioManager.instance.PlaySFX(stimSND);
                }
                Destroy(gameObject);
            }
        }
    }
}
