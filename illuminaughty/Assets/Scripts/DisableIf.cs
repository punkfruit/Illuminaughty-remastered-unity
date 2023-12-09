using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIf : MonoBehaviour
{
    public string gunkey;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString(gunkey) != "true")
        {
            gameObject.SetActive(false);
        }
    }

    
}
