using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inventory : MonoBehaviour
{
    public GameObject inventoryy;
    public Animator anim;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        MouseCursor.instance.on = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKey(KeyCode.E))
            {
                inventoryy.SetActive(true);
            }
            else
            {
                inventoryy.SetActive(false);
            }
        }
        else
        {
            inventoryy.SetActive(false);
        }
        
    }

    public void useTeleporter()
    {
        anim.SetTrigger("teleport");
        active = false;
    }

    public void GotoNextScene2(string scenee)
    {
        SceneManager.LoadScene(scenee);
        //PlayerController.instance.gameObject.SetActive(false);
    }
}
