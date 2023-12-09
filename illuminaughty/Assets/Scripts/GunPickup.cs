using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun theGun;
    public float waitToBeCollected = .5f;
    public int sound;
    private bool hasGun = false;
    private bool alreadyDone = false;
    public string gunKey;
    public Sprite gunSprite;
    public string thisGunName;
    public bool playPickupEffect;
    public GameObject pickupEffect;

    void Start()
    {
        if(PlayerPrefs.GetString(gunKey) == "true")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && waitToBeCollected <= 0 && !alreadyDone)
        {
            hasGun = false;
            foreach(Gun gunToCheck in PlayerController.instance.availableGuns)
            {
                if(theGun.name == gunToCheck.name)
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
            {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.instance.gunArm;
                gunClone.transform.position = PlayerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                PlayerController.instance.SwitchGun();

                PlayerPrefs.SetString(gunKey, "true");

                Destroy(gameObject);

                AudioManager.instance.PlaySFX(sound);


                if (playPickupEffect)
                {
                    Instantiate(pickupEffect);
                }
            }

            alreadyDone = true;
        }

        if (other.tag == "Player" && hasGun && waitToBeCollected <= 0)
        {
            Debug.Log("it happened again");
        }
    }
}
