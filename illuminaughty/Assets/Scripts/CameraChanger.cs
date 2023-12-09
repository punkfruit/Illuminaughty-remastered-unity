using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraChanger : MonoBehaviour
{
    public Camera cam;
    public PixelPerfectCamera pCam;
    //float timee = 1f;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        

        //Debug.Log(Screen.height);

        
    }

    public void backToNorm()
    {
        done = true;
        pCam.enabled = true;
        cam.orthographicSize = 5.625f;
    }

    private void Update()
    {
        //timee -= Time.deltaTime;
        if(cam == null)
        {
            cam = FindObjectOfType<Camera>();
            //
        }

        if(pCam == null)
        {
            pCam = FindObjectOfType<PixelPerfectCamera>();
        }



        if(cam != null && pCam != null && done == false)
        {
            chanCam();
        }
    }

    public void chanCam()
    {
        pCam.enabled = false;

        switch (Screen.height)
        {
            case 720:
                cam.orthographicSize = 5.625f;
                break;
            case 1080:
                cam.orthographicSize = 7.3f;
                break;
            case 1440:
                cam.orthographicSize = 10.5f;
                break;
            case 2160:
                cam.orthographicSize = 16.13125f;
                break;
            default:
                cam.orthographicSize = 20f;
                break;
        }

        if(pCam.enabled == false)
        {
           // done = true;
        }
    }
}
