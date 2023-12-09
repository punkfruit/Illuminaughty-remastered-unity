using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArm;
    public Transform spawnerPoint, cameraTarget;

    private Camera theCam;

    public Animator anim;

   // [HideInInspector]
    public bool canMove = true;

    public List<Gun> availableGuns = new List<Gun>();
    public int currentGun;

    [Header("block physics")]
    
    public List<GameObject> blocksInLevel = new List<GameObject>();

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

   

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this);
        canMove = true;
        theCam = Camera.main;
       // if(availableGuns[currentGun] != null)
     //   {
      //      UIcontroller.instance.gunImage.sprite = availableGuns[currentGun].gunSprite;
         //   UIcontroller.instance.gunText.text = availableGuns[currentGun].gunName;
       // }


    }

    // Update is called once per frame
    void Update()
    {
        //MouseWheelGun();
       
        if(theCam == null)
        {
            theCam = Camera.main;
        }

        if (canMove)
        {
            //player movement
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            theRB.velocity = moveInput * moveSpeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.localScale = new Vector3(-1f, -1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
                gunArm.localScale = Vector3.one;
            }

            //rotate gun arm
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (availableGuns.Count > 0)
                {
                    currentGun++;
                    if (currentGun >= availableGuns.Count)
                    {
                        currentGun = 0;
                    }
                    
                    SwitchGun();
                }
            }


            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }


            switchGunWithNumbers();

            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (availableGuns.Count > 0)
                {
                    currentGun++;
                    if (currentGun >= availableGuns.Count)
                    {
                        currentGun = 0;
                    }

                    SwitchGun();
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (availableGuns.Count > 0)
                {
                    currentGun--;
                    if (currentGun < 0)
                    {
                        currentGun = availableGuns.Count - 1;
                    }

                    SwitchGun();
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }

        
    }

    public void MouseWheelGun()
    {
        //currentGun += ((int)Input.GetAxisRaw("Mouse ScrollWheel"));

        //SwitchGun();

        /*if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            currentGun += ((int)Input.GetAxis("Mouse ScrollWheel"));

            SwitchGun();
        }*/

    }

    

    public void SwitchGun()
    {
        if(availableGuns.Count != 0)
        {
            foreach (Gun theGun in availableGuns)
            {
                theGun.gameObject.SetActive(false);
            }

            availableGuns[currentGun].gameObject.SetActive(true);
            UIcontroller.instance.gunImage.sprite = availableGuns[currentGun].gunSprite;
            UIcontroller.instance.gunText.text = availableGuns[currentGun].gunName;
        }
    }

    public void SwitchGunWithString(string namee)
    {
        if (availableGuns.Count > 0)
        {
            foreach (Gun theGun in availableGuns)
            {
                theGun.gameObject.SetActive(false);
            }

            for(int i = 0; i < availableGuns.Count; i++)
            {
                if(availableGuns[i].gunName == namee)
                {
                    availableGuns[i].gameObject.SetActive(true);
                    UIcontroller.instance.gunImage.sprite = availableGuns[i].gunSprite;
                    UIcontroller.instance.gunText.text = availableGuns[i].gunName;
                    currentGun = i;
                }
            }

            //availableGuns[gvn - 1].gameObject.SetActive(true);
            //UIcontroller.instance.gunImage.sprite = availableGuns[gvn - 1].gunSprite;
            //UIcontroller.instance.gunText.text = availableGuns[gvn - 1].gunName;
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void SwitchGunFromOut(int gvn)
    {
        if (availableGuns.Count > 0 && availableGuns.Count >= gvn)
        {
            foreach (Gun theGun in availableGuns)
            {
                theGun.gameObject.SetActive(false);
            }

            availableGuns[gvn - 1].gameObject.SetActive(true);
            UIcontroller.instance.gunImage.sprite = availableGuns[gvn - 1].gunSprite;
            UIcontroller.instance.gunText.text = availableGuns[gvn - 1].gunName;
        }
    }

    void switchGunWithNumbers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(availableGuns.Count > 0 && availableGuns.Count >= 1)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[0].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[0].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[0].gunName;

                currentGun = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 2)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[1].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[1].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[1].gunName;

                currentGun = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 3)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[2].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[2].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[2].gunName;

                currentGun = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 4)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[3].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[3].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[3].gunName;

                currentGun = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 5)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[4].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[4].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[4].gunName;

                currentGun = 4;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 6)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[5].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[5].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[5].gunName;

                currentGun = 5;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 7)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[6].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[6].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[6].gunName;

                currentGun = 6;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (availableGuns.Count > 0 && availableGuns.Count >= 8)
            {
                foreach (Gun theGun in availableGuns)
                {
                    theGun.gameObject.SetActive(false);
                }

                availableGuns[7].gameObject.SetActive(true);
                UIcontroller.instance.gunImage.sprite = availableGuns[7].gunSprite;
                UIcontroller.instance.gunText.text = availableGuns[7].gunName;

                currentGun = 7;
            }
        }
    }
}
