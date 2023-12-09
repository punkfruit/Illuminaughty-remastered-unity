using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonyFace : MonoBehaviour
{
    public GameObject mouth;
    public Transform openPos, closePos, launchPoint;
    public bool open, opened, shoot;
    public float coolDown, speed;
    private float counter;

    public GameObject projectile;
    public KonyMain parent;

    public float health;
    public int maxHealth;
    public GameObject death;
    public BigBossLaser laser;
    public Collider2D collid;
    // Start is called before the first frame update
    void Start()
    {
        mouth.transform.position = closePos.position;
        counter = coolDown;

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.active)
        {
            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }

            if (counter <= 0)
            {
                OpenShoot();
            }
            else
            {
                CloseMouth();
            }
        }

    }

    private void OpenShoot()
    {
        mouth.transform.position = Vector3.MoveTowards(mouth.transform.position, openPos.position, speed * Time.deltaTime);
        if(mouth.transform.position == openPos.position)
        {
            Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            counter = coolDown;
            AudioManager.instance.PlaySFX(36);
        }
    }

    private void CloseMouth()
    {
        mouth.transform.position = Vector3.MoveTowards(mouth.transform.position, closePos.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int dam, bool time)
    {
        if (time)
        {
            health -= dam * Time.deltaTime;
        }
        else
        {
            health -= dam;
        }
        

        UIcontroller.instance.bossHealthSlider.value = ((int)health);
        UIcontroller.instance.bossHealthText.text = UIcontroller.instance.bossHealthSlider.value + " / " + UIcontroller.instance.bossHealthSlider.maxValue;
        if(health <= 0)
        {
            parent.active = false;
            AudioManager.instance.StopAllMusic();
            parent.anim.enabled = true;
            collid.enabled = false;
            UIcontroller.instance.bossHealthOBJ.SetActive(false);
            //die
        }

    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet" || other.tag == "Laser")
        {
            TakeDamage(other.GetComponent<PlayerBullet>().damageToGive, false);
        }
    }
}
