using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;
    public bool initialized;

    //public bool destroyBulletsOnBecomeInvis = true;
    //public bool checkpointReached = false;
    public bool animationDone = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public bool checkpointReached()
    {
        if(PlayerPrefs.GetString("CHECKPOINT") == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
