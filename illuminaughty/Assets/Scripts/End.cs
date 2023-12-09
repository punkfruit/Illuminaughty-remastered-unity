using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public string title;
    public bool darn = false;
    void Start()
    {
        
        
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(title);
    }

    private void Update()
    {
        if (!darn)
        {
            Crap();
        }

        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }

    public void Crap()
    {
        if (UIcontroller.instance != null)
        {
            UIcontroller.instance.SelfDestruct();
        }
        if (CameraController.instance != null)
        {
            CameraController.instance.SelfDes();
        }
        if (PlayerController.instance != null)
        {
            PlayerController.instance.SelfDestruct();
        }

        CameraManager.instance.active = false;
        AudioManager.instance.PlayMainMusic(0);
        AudioManager.instance.constMusic = true;

        StartCoroutine("frick");
    }

    IEnumerator frick()
    {


        yield return new WaitForSeconds(1f);

        darn = true;
    }
    
}
