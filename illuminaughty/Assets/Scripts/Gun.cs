using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firePoint;

    public Sprite gunSprite;
    public string gunName;

    public float timeBetweenShots;
    private float shotCounter;

    public bool Auto;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //firebullet
        if (PlayerController.instance.canMove)
        {


            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots;

                }

                if (Auto)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (shotCounter <= 0)
                        {
                            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                            shotCounter = timeBetweenShots;
                        }
                    }
                }
            }
        }
    }

    public void Fire()
    {
       // if (shotCounter <= 0)
        //{
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

           // shotCounter = timeBetweenShots;
       // }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

   

}
