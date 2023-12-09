using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim : MonoBehaviour
{
    public int sound;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
        //AudioManager.instance.StopSFX(sound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        Debug.Log("touch");
    }
}
