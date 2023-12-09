using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickShimmer : MonoBehaviour
{
    public Animator anim;
    public int soundToPlay = 25;
    
    public void MedalClick()
    {
        anim.SetTrigger("activate");
    }

    public void PoppyHock()
    {
        AudioManager.instance.PlaySFXnormal(soundToPlay);
    }
    
}
