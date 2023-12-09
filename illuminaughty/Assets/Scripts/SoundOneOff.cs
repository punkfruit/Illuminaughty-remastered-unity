using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOneOff : MonoBehaviour
{
    public int soundToPlay;
    public Transform target;
    public bool followPlayer;
    public bool playOnStart;
    public bool destroyOverTime;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart)
        {
            AudioManager.instance.PlaySFX(soundToPlay);
        }
       

        if (followPlayer) { target = PlayerController.instance.transform; }

        if (destroyOverTime)
        {
            DestroyOverTime();
        }
    }

    private void Update()
    {
        if (followPlayer)
        { 
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.localScale = Vector3.one;
        }

        
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void PlaySound(int snd)
    {
        AudioManager.instance.PlaySFXnormal(snd);
    }

    public void DestroyOverTime()
    {
        Destroy(gameObject, time);
    }
}
