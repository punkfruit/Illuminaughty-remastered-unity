using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenGateCloser : MonoBehaviour
{

    public GardenGate gate;
    public Collider2D colid;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gate.CloseGate();
            colid.enabled = false;
        }
    }
}
