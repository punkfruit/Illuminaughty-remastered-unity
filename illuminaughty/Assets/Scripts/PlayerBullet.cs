using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f, lifeTime = 5f;
    public Rigidbody2D theRB;
    public GameObject impactEffect;
    public GameObject hitmark;
    public Transform center;

    public int damageToGive;
    public int sound;

    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(sound);
        counter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;

        counter -= Time.deltaTime;
        if(counter <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, center.position, transform.rotation);
        Destroy(gameObject);

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
            Instantiate(hitmark, center.position, transform.rotation);
        }
        if(other.tag == "Boss")
        {
            other.GetComponent<BossController>().DamageBoss(damageToGive);
            Instantiate(hitmark, center.position, transform.rotation);
        }
    }

    private void OnBecameInvisible()
    {
        //if (GeneralManager.instance.destroyBulletsOnBecomeInvis)
       // {
           // Destroy(gameObject);
       // }
        //Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (transform.parent != null) // if object has a parent
        {
            if (transform.parent.childCount <= 1) // if this object is the last child
            {
                Destroy(transform.parent.gameObject, 0.1f); // destroy parent a few frames later
            }
        }
    }
}
