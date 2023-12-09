using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutGunPage : MonoBehaviour
{
    public GameObject black, normal;
    public string gunKey;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString(gunKey) == "true")
        {
            black.gameObject.SetActive(false);
            normal.gameObject.SetActive(true);
        }
        else
        {
            black.gameObject.SetActive(true);
            normal.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetString(gunKey) == "true")
        {
            black.gameObject.SetActive(false);
            normal.gameObject.SetActive(true);
        }
        else
        {
            black.gameObject.SetActive(true);
            normal.gameObject.SetActive(false);
        }
    }
}
