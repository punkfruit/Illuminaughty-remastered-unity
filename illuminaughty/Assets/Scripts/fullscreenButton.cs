using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullscreenButton : MonoBehaviour
{
    public bool fullscreen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
