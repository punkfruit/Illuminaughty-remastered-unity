using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenGateKey : MonoBehaviour
{
    public GardenGate gate;
    public int snd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gate.AddKey();
            AudioManager.instance.PlaySFX(snd);
            Destroy(gameObject);
        }
    }
}
