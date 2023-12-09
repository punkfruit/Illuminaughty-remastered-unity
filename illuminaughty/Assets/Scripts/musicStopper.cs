using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicStopper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("mussto");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator mussto()
    {
        yield return new WaitForSeconds(0.5F);
        AudioManager.instance.StopAllMusic();
    }
}
