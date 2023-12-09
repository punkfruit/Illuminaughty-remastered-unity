using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteFade : MonoBehaviour
{
    public GameObject hole;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHole()
    {
        Instantiate(hole, new Vector3(10.669f, -11.156f, 0f), transform.rotation);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
