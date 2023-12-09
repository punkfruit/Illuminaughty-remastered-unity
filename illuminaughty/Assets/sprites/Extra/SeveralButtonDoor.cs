using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeveralButtonDoor : MonoBehaviour
{
    
    public bool[] checks;
    public bool multiBool
    {
        get
        {
            for (int i = 0; i < checks.Length; i++)
            {
                if (!checks[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public ScienceDoor door;
    private void Update()
    {
        door.open = multiBool;
        
    }
}
