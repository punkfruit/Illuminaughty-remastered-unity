using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPickupEffect : MonoBehaviour
{
    public Image gunImage;
    public Text gunName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelfDes()
    {
        Destroy(gameObject);
    }
}
