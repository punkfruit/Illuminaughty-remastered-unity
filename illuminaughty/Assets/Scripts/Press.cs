using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Press : MonoBehaviour
{
    public Animator anim;
    public Collider2D collid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UIcontroller.instance.gameObject.SetActive(false);
            anim.SetTrigger("Fade");
            collid.enabled = false;
        }
    }
}
